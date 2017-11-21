using FluentValidation.Attributes;
using Itransition.Task1.Web.Infrastructure.Validators;

namespace Itransition.Task1.Web.Models
{
    [Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}