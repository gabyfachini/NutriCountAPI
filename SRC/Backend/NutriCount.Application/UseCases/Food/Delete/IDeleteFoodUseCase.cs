namespace NutriCount.Application.UseCases.Food.Delete
{
    public interface IDeleteFoodUseCase
    {
        Task<bool> Execute(int id);
    }
}
