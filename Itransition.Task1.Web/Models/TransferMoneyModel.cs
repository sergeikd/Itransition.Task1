namespace Itransition.Task1.Web.Models
{

    public class TransferMoneyModel
    {
        public decimal TransferMoney { get; set; }

        public decimal OwnMoney { get; set; }
        public string SelectedAccount{ get; set; }

        //public List<SelectListItem> Accounts { get; set; }
    }
}