using FluentValidation;
using NutriCount.Exceptions;

namespace NutriCount.Application.UseCases.Foods.Delete
{
    public class DeleteFoodValidator : AbstractValidator<Guid>
    {
        public DeleteFoodValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage(ResourceMessageException.ID_EMPTY);
        }
    }
}
