using System;
using System.Linq;
using FluentValidation;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Infrastructure.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly IUserService _userService;

        public RegisterModelValidator(IBankAccountService bankAccountService, IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException();
            _userService = userService;

            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u).Must(CheckPasswords).WithMessage("Password and Confirm Password are not equal");
            RuleFor(u => u).Must(IsExist).WithMessage("User with the same name already exists");
        }

        private static bool CheckPasswords(RegisterModel registerModel)
        {
            return registerModel.Password == registerModel.ConfirmPassword;
        }
        private bool IsExist(RegisterModel registerModel)
        {
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == registerModel.Email);
            return user == null;
        }
    }
}