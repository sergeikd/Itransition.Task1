using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBankAccountService _bankAccountService; 

        public HomeController(IBankAccountService bankAccountService)
        {
            if(bankAccountService == null) throw new ArgumentNullException();
            _bankAccountService = bankAccountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Default()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData()
        {
            return Json(_bankAccountService.GetGlobalData(User.Identity.Name));
        }
        [HttpPost]
        public JsonResult Put(string put)
        {
            return Json(_bankAccountService.PutMoney(User.Identity.Name, put));
        }

        [HttpPost]
        public JsonResult Transfer(string transfer, string recipient)
        {
            var aaa = _bankAccountService.TransferMoney(User.Identity.Name, transfer, recipient);
            return Json(_bankAccountService.TransferMoney(User.Identity.Name, transfer, recipient));
        }
    }
}