using System;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.WebCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Task1.WebCore.Controllers
{
    [AllowAnonymous]
    public class PasswordController : Controller
    {
        private readonly IPasswordService _resetService;
        private readonly IUserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PasswordController(IPasswordService resetService, IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            if (resetService == null) throw new ArgumentNullException();
            if (userService == null) throw new ArgumentNullException();
            if (hostingEnvironment == null) throw new ArgumentNullException();
            _resetService = resetService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var linkUri = _hostingEnvironment.WebRootPath;
            _resetService.SendForgotPasswordEmail(linkUri, model.Email);
            return RedirectToAction("ResetMsg", "Password");
        }

        [HttpGet]
        public ActionResult ResetResult(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.ChangePassword(model.Email, "qwerty");
                _resetService.RemoveResets(model.Email);
            }
            return View("ResetResult", ModelState.IsValid);
        }

        public ActionResult ResetMsg()
        {
            return View("ResetMsg");
        }
    }
}