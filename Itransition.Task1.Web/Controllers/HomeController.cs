using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Itransition.Task1.DAL;
using Itransition.Task1.DAL.Repositories;
using Itransition.Task1.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Itransition.Task1.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            decimal? amount = null;
            var bankAccountRepository = new BankAccountRepository(new AppDbContext());
            var accountList = bankAccountRepository.GetAll().ToList();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                amount = user.BankAccount.Amount;
            }
            return View(amount);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}