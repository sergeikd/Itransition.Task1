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
        public JsonResult GetGlobalData()
        {
            var globalData = _bankAccountService.GetInitGlobalData(User.Identity.Name);
            var globalDataModel = new GlobalDataModel
            {
                UserAmount = globalData.Amount,
                OthersAccounts = globalData.OthersAccountNumbers,
                ErrorMsg = globalData.ErrorMsg
            };

            return Json(globalDataModel);
        }
        [HttpPost]
        public JsonResult Put(string put)
        {
            var globalData = _bankAccountService.PutMoney(User.Identity.Name, put);
            var globalDataModel = new GlobalDataModel
            {
                UserAmount = globalData.Amount,
                OthersAccounts = globalData.OthersAccountNumbers,
                ErrorMsg = globalData.ErrorMsg
            };
            return Json(globalDataModel);
        }

        [HttpPost]
        public JsonResult Transfer(string transfer, string recipient)
        {
            var globalData = _bankAccountService.TransferMoney(User.Identity.Name, transfer, recipient);
            var globalDataModel = new GlobalDataModel
            {
                UserAmount = globalData.Amount,
                OthersAccounts = globalData.OthersAccountNumbers,
                ErrorMsg = globalData.ErrorMsg
            };
            return Json(globalDataModel);
        }
    }
}