using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Itransition.Task1.Web.Infrastructure.Validators;

namespace Itransition.Task1.Web.Models
{
    [Validator(typeof(PutMoneyModelValidator))]
    public class PutMoneyModel
    {
        [Display(Name = "Input sum")]
        public decimal Amount { get; set; }
    }
}