using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Domain;
using Itransition.Task1.WebCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Task1.WebCore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException();
            _userService = userService;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await Authenticate(model.Email);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _userService.RegisterUser(new AppUser { Email = model.Email, Password = model.Password });
            await Authenticate(model.Email);
            return RedirectToAction("Index", "Home");
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Auth");
        }
    }
}