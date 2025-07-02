using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Repositories.Food
{
    public interface IFoodReadOnlyRepository
    {
        Task<Entities.Eating?> GetByIdAsync(int id);
        Task<IEnumerable<Entities.Eating>> GetAllAsync();
    }
}
