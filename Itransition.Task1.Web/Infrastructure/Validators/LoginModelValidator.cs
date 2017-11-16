using System;
using System.Linq;
using FluentValidation;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Infrastructure.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private readonly IUserService _userService;

        public LoginModelValidator(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException();
            _userService = userService;

            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u).Must(IsExist).WithMessage("No user with such login or password is wrong");
        }
        private bool IsExist(LoginModel loginModel)
        {
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Name == loginModel.Name && u.Password == loginModel.Password);
            return user != null;
        }
    }
}