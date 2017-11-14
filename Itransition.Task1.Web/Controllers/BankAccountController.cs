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
        private string _currentUserName;

        public BankAccountController(IBankAccountService bankAccountService, IUserService userService)
        {
            _bankAccountService = bankAccountService ?? throw new ArgumentNullException();
            _userService = userService ?? throw new ArgumentNullException();
            _currentUserName =  User.Identity.Name;
        }
        public ActionResult Put()
        {
            var model = new PutMoneyModel{Amount = _userService.GetUserAmount(_currentUserName) };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Put(PutMoneyModel model)
        {
            if (ModelState.IsValid)
            {
                _bankAccountService.PutMoney(_currentUserName, model.Amount);
                return RedirectToAction("Put");
            }
            model.Amount = _userService.GetUserAmount(_currentUserName);
            return View(model);
        }

        public ActionResult Transfer()
        {
            var model = new TransferMoneyModel
            {
                Amount = _userService.GetUserAmount(_currentUserName),
                Accounts = PrepareAccountsDropDownList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferMoneyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bankAccountService.TransferMoney(_currentUserName, model.Amount, model.SelectedAccount);
                    return RedirectToAction("Transfer");
                }
                catch (ArgumentOutOfRangeException)
                {
                    ModelState.AddModelError("Amount", "Insufficient money");
                }
            }
            model.Accounts = PrepareAccountsDropDownList();
            return View(model);
        }

        internal List<SelectListItem> PrepareAccountsDropDownList()
        {
            var currentUser = _userService.GetCurrentUser(_currentUserName);
            var accounts = _bankAccountService.GetAllBankAccounts().Where(x => x.AccountNumber != currentUser.BankAccount.AccountNumber); //Remove own account from the list
            return accounts.Select(account => new SelectListItem { Text = account.AccountNumber}).ToList();
        }
    }
}