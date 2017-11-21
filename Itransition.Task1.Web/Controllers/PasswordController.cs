using System;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Controllers
{
    [AllowAnonymous]
    public class PasswordController : Controller
    {
        private readonly IPasswordService _resetService;
        private readonly IUserService _userService;

        public PasswordController(IPasswordService resetService, IUserService userService)
        {
            if (resetService == null) throw new ArgumentNullException();
            if (userService == null) throw new ArgumentNullException();
            _resetService = resetService;
            _userService = userService;
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
            var linkUri = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
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