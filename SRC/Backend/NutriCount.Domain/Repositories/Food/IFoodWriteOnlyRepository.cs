using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Repositories.Food
{
    public interface IFoodWriteOnlyRepository
    {
        Task AddAsync(Entities.Eating food);
        Task UpdateAsync(Entities.Eating food);
        Task DeleteAsync(int id);
    }
}
