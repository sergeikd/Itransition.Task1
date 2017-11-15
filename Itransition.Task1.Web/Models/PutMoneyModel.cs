using System.ComponentModel.DataAnnotations;

namespace Itransition.Task1.Web.Models
{
    public class PutMoneyModel
    {
        [Display(Name = "Input sum")]
        public decimal Amount { get; set; }
    }
}