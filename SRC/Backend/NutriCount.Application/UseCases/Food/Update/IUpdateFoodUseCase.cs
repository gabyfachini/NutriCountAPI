using NutriCount.Communication.Request;

namespace NutriCount.Application.UseCases.Food.Update
{
    public interface IUpdateFoodUseCase
    {
        /// <summary>
        /// Executa a atualização. Retorna false se não encontrar o alimento.
        /// </summary>
        Task<bool> Execute(int id, RequestFoodUpdateJson request);
    }
}
