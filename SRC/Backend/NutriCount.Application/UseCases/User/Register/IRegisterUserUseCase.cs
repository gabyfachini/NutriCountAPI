using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
