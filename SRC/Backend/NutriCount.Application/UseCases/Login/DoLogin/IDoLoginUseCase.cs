using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
