using FluentValidation.Attributes;
using Itransition.Task1.WebCore.Validators;

namespace Itransition.Task1.WebCore.Models
{
    [Validator(typeof(ForgotPasswordModelValidator))]
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}