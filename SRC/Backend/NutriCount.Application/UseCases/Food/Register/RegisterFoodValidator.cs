using FluentValidation;
using NutriCount.Communication.Request;
using NutriCount.Exceptions;

namespace NutriCount.Application.UseCases.Foods.Register
{
    public class RegisterFoodValidator : AbstractValidator<RequestFoodRegisterJson>
    {
        public RegisterFoodValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);

            RuleFor(x => x.ServingSize)
                .GreaterThan(0).WithMessage("Serving size must be greater than zero.");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("Calories cannot be negative.");

            RuleFor(x => x.Proteins)
                .GreaterThanOrEqualTo(0).WithMessage("Proteins cannot be negative.");

            RuleFor(x => x.Carbohydrates)
                .GreaterThanOrEqualTo(0).WithMessage("Carbohydrates cannot be negative.");

            RuleFor(x => x.TotalFats)
                .GreaterThanOrEqualTo(0).WithMessage("Total fats cannot be negative.");

            RuleFor(x => x.SaturatedFats)
                .GreaterThanOrEqualTo(0).WithMessage("Saturated fats cannot be negative.");

            RuleFor(x => x.TransFats)
                .GreaterThanOrEqualTo(0).WithMessage("Trans fats cannot be negative.");
        }
    }
}
