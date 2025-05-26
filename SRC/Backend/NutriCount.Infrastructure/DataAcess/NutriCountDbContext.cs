using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Entities;

namespace NutriCount.Infrastructure.DataAcess
{
    public class NutriCountDbContext : DbContext
    {
        public NutriCountDbContext(DbContextOptions options) : base(options) { } //construtor para receber base do banco de dados
        public DbSet<User> Users { get; set; }
        public object User { get; internal set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutriCountDbContext).Assembly);
        }
    }
}