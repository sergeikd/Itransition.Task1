using FluentValidation;
using Itransition.Task1.Web.Models;

namespace Itransition.Task1.Web.Infrastructure.Validators
{
    public class PutMoneyModelValidator : AbstractValidator<PutMoneyModel>
    {
        public PutMoneyModelValidator()
        {
            RuleFor(u => u.Amount).NotNull()
                .Must(IsInRange).WithMessage("Input value must be more than 0 and less than 100");
        }
        private static bool IsInRange(decimal amount)
        {
            return amount > 0 && amount <= 100;
        }
    }
}