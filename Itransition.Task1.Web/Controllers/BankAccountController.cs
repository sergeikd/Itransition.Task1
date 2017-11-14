using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;


namespace Itransition.Task1.Web.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IUserService _userService;

        public BankAccountController(IBankAccountService bankAccountService, IUserService userService)
        {
            _bankAccountService = bankAccountService ?? throw new ArgumentNullException();
            _userService = userService ?? throw new ArgumentNullException();
        }
        public ActionResult Put()
        {
            var model = new PutMoneyModel{Amount = _userService.GetUserAmount(User.Identity.Name)};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Put(PutMoneyModel model)
        {
            if (ModelState.IsValid)
            {
                _bankAccountService.PutMoney(User.Identity.Name, model.Amount);
                return RedirectToAction("Put");
            }
            model.Amount = _userService.GetUserAmount(User.Identity.Name);
            return View(model);
        }

        public ActionResult Transfer()
        {
            var model = new TransferMoneyModel();
            var user = _userService.GetCurrentUser(User.Identity.Name);
            model.Amount = user.BankAccount.Amount;
            model.Accounts = PrepareCompaniesDropDownList(user.BankAccount.AccountNumber);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferMoneyModel model)
        {
            var user = _userService.GetCurrentUser(User.Identity.Name);
            if (ModelState.IsValid)
            {
                try
                {
                    _bankAccountService.TransferMoney(user, model.Amount, model.SelectedAccount);
                    return RedirectToAction("Transfer");
                }
                catch (ArgumentOutOfRangeException)
                {
                    ModelState.AddModelError("Amount", "Insufficient money");
                }
            }
            model.Accounts = PrepareCompaniesDropDownList(user.BankAccount.AccountNumber);
            return View(model);
        }

        internal List<SelectListItem> PrepareCompaniesDropDownList(string ownAccount)
        {
            var accounts = _bankAccountService.GetAllBankAccounts().Where(x => x.AccountNumber != ownAccount); //Remove own account from the list
            return accounts.Select(account => new SelectListItem { Text = account.AccountNumber}).ToList();
        }
    }
}