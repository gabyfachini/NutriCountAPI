using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            Validate(request);
            //Se a validação for positiva, tem que mapear a request uma entidade
            //Depois criptografa a senha e salva no banco de dados

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
