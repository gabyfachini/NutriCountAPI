using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.Food.Register
{
    public interface IRegisterFoodUseCase
    {
        Task<ResponseFoodJson> Execute(RequestFoodRegisterJson request);
    }
}
