using FluentValidation.Attributes;
using Itransition.Task1.WebCore.Validators;

namespace Itransition.Task1.WebCore.Models
{
    [Validator(typeof(ResetPasswordModelValidator))]
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}