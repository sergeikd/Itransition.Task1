using System;
using Itransition.Task1.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Itransition.Task1.WebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankAccountService _bankAccountService;

        public HomeController(IBankAccountService bankAccountService)
        {
            if (bankAccountService == null) throw new ArgumentNullException();
            _bankAccountService = bankAccountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(int pageSize, int currentPage, string sort)
        {
            return Json(_bankAccountService.GetGlobalData(User.Identity.Name, pageSize, currentPage, sort));
        }
        [HttpPost]
        public JsonResult Put(string put)
        {
            return Json(_bankAccountService.PutMoney(User.Identity.Name, put));
        }

        [HttpPost]
        public JsonResult Transfer(string transfer, string recipient)
        {
            return Json(_bankAccountService.TransferMoney(User.Identity.Name, transfer, recipient));
        }
    }
}
