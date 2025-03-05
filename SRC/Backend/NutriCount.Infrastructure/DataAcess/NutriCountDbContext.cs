using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NutriCount.Domain.Entities;

namespace NutriCount.Infrastructure.DataAcess
{
    public class NutriCountDbContext : DbContext
    {
        public NutriCountDbContext(DbContextOptions options) : base(options) { } //construtor para receber base do banco de dados
        public DbSet<User> Users { get; set; }
        public object User { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutriCountDbContext).Assembly);
        }
    }
}