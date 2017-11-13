using System.ComponentModel.DataAnnotations;

namespace Itransition.Task1.Web.Models
{
    public class PutMoneyModel
    {
        [Required]
        [Display(Name = "Input sum")]
        [Range(typeof(decimal), "0", "100")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Incorrect input")]
        public decimal Amount { get; set; }
    }
}