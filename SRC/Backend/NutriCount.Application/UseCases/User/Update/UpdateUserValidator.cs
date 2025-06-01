using FluentValidation;
using NutriCount.Communication.Request;
using NutriCount.Exceptions;
using NutriCount.Domain.Extensions;

namespace NutriCount.Application.UseCases.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);

            When(request => request.Email.NotEmpty(), () =>
            {
                RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            });
        }
    }
}
