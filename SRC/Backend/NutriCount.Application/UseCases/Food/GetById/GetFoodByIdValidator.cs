using FluentValidation;
using NutriCount.Exceptions;

namespace NutriCount.Application.UseCases.Foods.GetById
{
    public class GetFoodByIdValidator : AbstractValidator<Guid>
    {
        public GetFoodByIdValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage(ResourceMessageException.GetID_EMPTY());
        }
    }
}
