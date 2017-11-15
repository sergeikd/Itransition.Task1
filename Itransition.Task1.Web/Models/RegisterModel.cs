using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Itransition.Task1.Web.Infrastructure.Validators;

namespace Itransition.Task1.Web.Models
{
    [Validator(typeof(LoginModelValidator))]
    public class RegisterModel
    {
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}