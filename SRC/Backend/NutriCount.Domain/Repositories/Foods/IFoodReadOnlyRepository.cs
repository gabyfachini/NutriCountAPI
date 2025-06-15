using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Repositories.Food
{
    public interface IFoodReadOnlyRepository
    {
        Task<Entities.Foods?> GetByIdAsync(int id);
        Task<IEnumerable<Entities.Foods>> GetAllAsync();
    }
}
