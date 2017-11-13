using System.ComponentModel.DataAnnotations;

namespace Itransition.Task1.Web.Models
{
    public class PutMoneyModel
    {
        [Required]
        [Display(Name = "Input sum")]
        [Range(typeof(decimal), "0", "100")]
        public decimal Amount { get; set; }
    }
}