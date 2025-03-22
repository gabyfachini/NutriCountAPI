﻿using CommonTestUtilities.Requests;
using NutriCount.Application.UseCases.User.Register;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    { 
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);
            
            /*result.IsValid.Should().BeTrue();*/

        }
    }
}
