using System;
using System.Web;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfases;
using Itransition.Task1.Domain;
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
            var user = new ApplicationUser();
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    _bankAccountService.PutMoney(user, (decimal)model.Amount);
                }
                return RedirectToAction("Put");
            }
            user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            model.Amount = user.BankAccount.Amount;
            return View(model);
        }

        public ActionResult Transfer()
        {
            return View();
        }
    }
}