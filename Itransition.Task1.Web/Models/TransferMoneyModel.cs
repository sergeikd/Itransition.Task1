using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Itransition.Task1.Web.Models
{
    public class TransferMoneyModel
    {
        [Required]
        [Display(Name = "Input sum")]
        [Range(typeof(decimal), "0", "100")]
        public decimal Amount { get; set; }

        public string SelectedAccount{ get; set; }

        [Display(Name = "Select account")]
        public List<SelectListItem> Accounts { get; set; }
    }
}