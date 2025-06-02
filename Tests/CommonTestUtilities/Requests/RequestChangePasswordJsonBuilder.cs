﻿using Bogus;
using NutriCount.Communication.Request;

namespace CommonTestUtilities.Requests
{
    public class RequestChangePasswordJsonBuilder
    {
        public static RequestChangePasswordJson Build(int passwordLength = 10)
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(u => u.Password, (f) => f.Internet.Password())
                .RuleFor(u => u.NewPassword, (f) => f.Internet.Password(passwordLength));
        }
    }
}
