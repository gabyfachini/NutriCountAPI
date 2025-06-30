using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Repositories.Food;

namespace NutriCount.Infrastructure.DataAcess.Repositories
{
    public sealed class FoodRepository : IFoodWriteOnlyRepository, IFoodReadOnlyRepository //essas interfaces estao no domain, ver o que esta dando erro
    {
        private readonly NutriCountDbContext _dbContext;

        public FoodRepository(NutriCountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Registrar novo alimento
        public async Task AddAsync(Foods food)
        {
            await _dbContext.Foods.AddAsync(food);
        }

        // Deletar alimento por ID
        public async Task DeleteAsync(int foodId)
        {
            var food = await _dbContext.Foods.FindAsync(foodId);

            if (food is not null)
                _dbContext.Foods.Remove(food);
        }

        // Buscar alimento por ID
        public async Task<Foods?> GetByIdAsync(int foodId)
        {
            return await _dbContext
                .Foods
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == foodId);
        }

        // Buscar todos os alimentos
        public async Task<IEnumerable<Foods>> GetAllAsync()
        {
            return await _dbContext
            .Foods
            .AsNoTracking()
                .ToListAsync();
        }

        // Atualizar alimento
        public async Task UpdateAsync(Foods food)
        {
            _dbContext.Foods.Update(food);
        }
    }
}
