using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Itransition.Task1.WebCore.Validators;

namespace Itransition.Task1.WebCore.Models
{
    [Validator(typeof(LoginModelValidator))]
    public class LoginModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
