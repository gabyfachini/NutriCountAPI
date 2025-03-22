using NutriCount.Application.UseCases.User.Register;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            //result.IsValid == true

        }
    }
}
