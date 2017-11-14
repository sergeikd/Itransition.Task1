using System;
using System.Linq;
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

        public AuthController(IBankAccountService bankAccountService, IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Name == model.Name && u.Password == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "No user with such login or password is wrong");
                }
            }
            
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAllUsers().FirstOrDefault(u => u.Name == model.Name);
                if (user == null)
                {                      
                    _userService.RegisterUser(new AppUser{Name = model.Name, Password = model.Password});
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User with the same name already exists");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}