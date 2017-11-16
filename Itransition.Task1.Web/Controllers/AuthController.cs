using System;
using System.Web.Mvc;
using System.Web.Security;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Domain;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException();
            _userService = userService;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            FormsAuthentication.SetAuthCookie(model.Name, true);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _userService.RegisterUser(new AppUser { Name = model.Name, Password = model.Password });
            FormsAuthentication.SetAuthCookie(model.Name, true);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}