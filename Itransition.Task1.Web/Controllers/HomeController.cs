using System;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;

namespace Itransition.Task1.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IUserService _userService; 

        public HomeController(IBankAccountService bankAccountService, IUserService userService)
        {
            _bankAccountService = bankAccountService ?? throw new ArgumentNullException();
            _userService = userService ?? throw new ArgumentNullException();
        }
        public ActionResult Index()
        {
            decimal amount = 0;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var currentName = System.Web.HttpContext.Current.User.Identity.Name;
                var user = _userService.GetCurrentUser(currentName);
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