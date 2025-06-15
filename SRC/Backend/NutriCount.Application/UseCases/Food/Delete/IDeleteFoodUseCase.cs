namespace NutriCount.Application.UseCases.Food.Delete
{
    public interface IDeleteFoodUseCase
    {
        /// <summary>
        /// Executa a remoção. Retorna false se não encontrar o alimento.
        /// </summary>
        Task<bool> Execute(int id);
    }
}
