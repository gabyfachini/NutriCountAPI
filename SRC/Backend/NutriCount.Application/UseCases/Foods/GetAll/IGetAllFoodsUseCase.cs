using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.Food.GetAll
{
    public interface IGetAllFoodsUseCase
    {
        Task<IEnumerable<ResponseFoodJson>> Execute();
    }
}
