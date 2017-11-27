using System;
using System.Web.Mvc;
using Itransition.Task1.BL.Interfaces;

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

        [HttpPost]
        public JsonResult GetData(int pageSize, int currentPage)
        {
            return Json(_bankAccountService.GetGlobalData(User.Identity.Name, pageSize, currentPage));
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