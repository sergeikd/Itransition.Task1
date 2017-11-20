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
        //public ActionResult Login()
        //{
        //    return View();
        //}
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            FormsAuthentication.SetAuthCookie(model.Email, true);
            return RedirectToAction("Index", "Home");
        }
        

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);
        //    FormsAuthentication.SetAuthCookie(model.Email, true);
        //    return RedirectToAction("Index", "Home");
        //}

        //public ActionResult Register()
        //{
        //    return View();
        //}

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _userService.RegisterUser(new AppUser { Email = model.Email, Password = model.Password });
            FormsAuthentication.SetAuthCookie(model.Email, true);
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);
        //    _userService.RegisterUser(new AppUser { Email = model.Email, Password = model.Password });
        //    FormsAuthentication.SetAuthCookie(model.Email, true);
        //    return RedirectToAction("Index", "Home");
        //}
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}