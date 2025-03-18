using NutriCount.Domain.Repositories;

namespace NutriCount.Infrastructure.DataAcess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NutriCountDbContext _dbContext;
        public UnitOfWork(NutriCountDbContext dbContext) => _dbContext = dbContext;
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
