﻿using FluentValidation;
using NutriCount.Communication.Request;
using NutriCount.Exceptions;

namespace NutriCount.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() //Validações das propriedades
        { 
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageException.PASSWORD_EMPTY);
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            });
        }
    }
}
