using NutriCount.Application.Services.AutoMapper;
using NutriCount.Application.Services.Cryptography;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            var criptografiaDeSenha = new PasswordEncripter();
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            Validate(request);

            var user = autoMapper.Map<Domain.Entities.User>(request);
            user.Password = criptografiaDeSenha.Encrypt(request.Password); 

            //salva no banco de dados

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
