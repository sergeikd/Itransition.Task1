using System;
using System.Web;
using FluentValidation;
using Itransition.Task1.BL.Interfaces;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Infrastructure.Validators
{
    public class TransferMoneyModelValidator : AbstractValidator<TransferMoneyModel>
    {
        private readonly IUserService _userService;
        private readonly string _currentUserName;

        public TransferMoneyModelValidator( IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException();
            _currentUserName = HttpContext.Current.User.Identity.Name;
            RuleFor(u => u.TransferMoney).NotNull()
                .Must(IsInRange).WithMessage("Input value must be more than 0 and less than 100")
                .Must(IsMoneyEnough).WithMessage("Isufficient money");

        }
        private bool IsMoneyEnough(decimal amount)
        {
            var currentAmount = _userService.GetUserAmount(_currentUserName);
            return currentAmount >= amount;
        }
        private static bool IsInRange(decimal amount)
        {
            return amount > 0 && amount <= 100;
        }
    }
}