using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NutriCount.Domain.Repositories.User;
using NutriCount.Infrastructure.DataAcess;
using NutriCount.Infrastructure.DataAcess.Repositories;

namespace NutriCount.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInsfrastructure (this IServiceCollection services)
        {
            AddDbContext_MySqlServer(services);
            AddRepositories(services);
        }
        private static void AddDbContext_MySqlServer(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database= ;Uid= ;Pwd= ;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContext<NutriCountDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }
        private static void AddDbContext_sqlServer(IServiceCollection services)
        {
            var connectionString = "Data SOURCE=DESKTOP-4NNJ20L;Initial Catalog=NutriCount;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"; 

            services.AddDbContext<NutriCountDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
