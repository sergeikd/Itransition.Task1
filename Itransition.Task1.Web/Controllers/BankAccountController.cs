using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Itransition.Task1.Web.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService ?? throw new ArgumentNullException();
        }
        public ActionResult Put()
        {
            var model = new PutMoneyModel();
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            model.Amount = user.BankAccount.Amount;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Put(PutMoneyModel model)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                _bankAccountService.PutMoney(user, model.Amount);
                return RedirectToAction("Put");
            }
            model.Amount = user.BankAccount.Amount;
            return View(model);
        }

        public ActionResult Transfer()
        {
            var model = new TransferMoneyModel();
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            model.Amount = user.BankAccount.Amount;
            model.Accounts = PrepareCompaniesDropDownList(user.BankAccount.AccountNumber);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferMoneyModel model)
        {
            var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
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