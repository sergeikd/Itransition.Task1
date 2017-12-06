using System;
using System.Linq;
using FluentValidation;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.WebCore.Models;

namespace Itransition.Task1.WebCore.Validators
{
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        private readonly IUserService _userService;

        public ForgotPasswordModelValidator(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException();
            _userService = userService;

            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress()
                .Must(IsExist).WithMessage("Such Email is not registered");
        }

        private bool IsExist(string email)
        {
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == email);
            return user != null;
        }
    }
}