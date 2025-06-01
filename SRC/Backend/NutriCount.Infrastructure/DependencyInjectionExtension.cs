using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutriCount.Domain.Enums;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Domain.Services.LoggedUser;
using NutriCount.Infrastructure.DataAcess;
using NutriCount.Infrastructure.DataAcess.Repositories;
using NutriCount.Infrastructure.Extensions;
using NutriCount.Infrastructure.Secutiry.Tokens.Access.Generator;
using NutriCount.Infrastructure.Secutiry.Tokens.Access.Validator;
using NutriCount.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace NutriCount.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddLoggedUser(services);
            AddTokens(services, configuration);

            if (configuration.IsUnitTestEnviroment())
                return;
            
            var databaseType = configuration.DatabaseType();

            if (databaseType == DatabaseType.MySql)
            {
                AddDbContext_MySqlServer(services, configuration);
                AddFluentMigrator_MySql(services, configuration);
            }
            else
            {
                AddDbContext_sqlServer(services, configuration);
                AddFluentMigrator_SqlServer(services, configuration);
            }

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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("NutriCount.Infrastructure")).For.All();
            });
        }
        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("NutriCount.Infrastructure")).For.All();
            });
        }
        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
        }
        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
    }
}
