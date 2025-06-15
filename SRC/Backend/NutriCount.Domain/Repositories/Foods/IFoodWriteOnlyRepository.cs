using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Repositories.Food
{
    public interface IFoodWriteOnlyRepository
    {
        Task AddAsync(Entities.Foods food);
        Task UpdateAsync(Entities.Foods food);
        Task DeleteAsync(int id);
    }
}
