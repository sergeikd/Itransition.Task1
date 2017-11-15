using System;
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
            var model = new PutMoneyModel{Amount = _userService.GetUserAmount(User.Identity.Name) };
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
            return View(DefaultTransferMoneyModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferMoneyModel model)
        {
            if (!ModelState.IsValid) return View(DefaultTransferMoneyModel());
            _bankAccountService.TransferMoney(User.Identity.Name, model.TransferMoney, model.SelectedAccount);
            return RedirectToAction("Transfer");
        }

        private TransferMoneyModel DefaultTransferMoneyModel()
        {
            var currentUser = _userService.GetCurrentUser(User.Identity.Name);
            var accounts = _bankAccountService.GetAllBankAccounts().Where(x => x.AccountNumber != currentUser.BankAccount.AccountNumber); //Remove own account from the list

            var model = new TransferMoneyModel
            {
                OwnMoney = _userService.GetUserAmount(User.Identity.Name),
                TransferMoney = 0,
                Accounts = accounts.Select(account => new SelectListItem { Text = account.AccountNumber }).ToList()
            };
            return model;
        }
    }
}