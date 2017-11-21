using System;
using System.Linq;
using FluentValidation;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Infrastructure.Validators
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        private readonly IPasswordService _passwordService;


        public ResetPasswordModelValidator(IPasswordService passwordService)
        {
            if (passwordService == null) throw new ArgumentNullException();
            _passwordService = passwordService;

            RuleFor(p => p).NotNull().Must(IsExist);
            RuleFor(p => p.Email).NotEmpty()
                .EmailAddress();
            RuleFor(p => p.Code).NotEmpty();
        }

        private bool IsExist(ResetPasswordModel model)
        {
            var result = _passwordService.GetAllResets().FirstOrDefault(p => p.Email == model.Email && p.Code == model.Code);
            return result != null;
        }
    }
}