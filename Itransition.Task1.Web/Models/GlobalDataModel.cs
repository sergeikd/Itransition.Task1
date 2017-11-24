using System.Collections.Generic;
using System.Web.Mvc;
using Itransition.Task1.BL.DtoModels;

namespace Itransition.Task1.Web.Models
{
    public class GlobalDataModel
    {
        public decimal UserAmount { get; set; }
        public List<string> OthersAccounts { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public string ErrorMsg { get; set; }
    }
}