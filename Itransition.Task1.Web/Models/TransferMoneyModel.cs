using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Itransition.Task1.Web.Infrastructure.Validators;

namespace Itransition.Task1.Web.Models
{
    [Validator(typeof(TransferMoneyModelValidator))]
    public class TransferMoneyModel
    {
        [Display(Name = "Input sum")]
        public decimal TransferMoney { get; set; }

        public decimal OwnMoney { get; set; }
        public string SelectedAccount{ get; set; }

        [Display(Name = "Select account")]
        public List<SelectListItem> Accounts { get; set; }
    }
}