using System.Collections.Generic;
using System.Web.Mvc;

namespace Itransition.Task1.Web.Models
{
    public class GlobalDataModel
    {
        public decimal UserAmount { get; set; }
        public List<string> OthersAccounts { get; set; }
        public string ErrorMsg { get; set; }
    }
}