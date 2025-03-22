using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutriCount.Domain.Enums;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.User;
using NutriCount.Infrastructure.DataAcess;
using NutriCount.Infrastructure.DataAcess.Repositories;
using NutriCount.Infrastructure.Extensions;

namespace NutriCount.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == DatabaseType.MySql)
                AddDbContext_MySqlServer(services, configuration);
            else
                AddDbContext_sqlServer(services, configuration);

            AddRepositories(services);
        }
        private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            services.AddDbContext<NutriCountDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }
        private static void AddDbContext_sqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<NutriCountDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, IUnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
