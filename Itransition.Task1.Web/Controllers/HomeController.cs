using System;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;

namespace Itransition.Task1.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService; 

        public HomeController(IUserService userService)
        {
            if(userService == null) throw new ArgumentNullException();
            _userService = userService;
        }
        public ActionResult Index()
        {
            decimal amount = 0;
            if (User.Identity.IsAuthenticated)
            {
                amount = _userService.GetUserAmount(User.Identity.Name);
            }
            return View(amount);
        }
    }
}