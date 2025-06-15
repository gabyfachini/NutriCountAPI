using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.Food.GetById
{
    public interface IGetFoodByIdUseCase
    {
        Task<ResponseFoodJson?> Execute(int id);
    }
}
