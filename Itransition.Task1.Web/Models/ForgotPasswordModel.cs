using FluentValidation.Attributes;
using Itransition.Task1.Web.Infrastructure.Validators;

namespace Itransition.Task1.Web.Models
{
    [Validator(typeof(ForgotPasswordModelValidator))]
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}