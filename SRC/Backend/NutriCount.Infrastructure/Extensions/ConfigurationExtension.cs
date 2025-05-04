using Microsoft.Extensions.Configuration;
using NutriCount.Domain.Enums;

namespace NutriCount.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
        public static DatabaseType DatabaseType(this IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");
            return (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType!);
        }
        public static string ConnectionString(this IConfiguration configuration)
        {
            var databaseType = configuration.DatabaseType();

            if (databaseType == Domain.Enums.DatabaseType.MySql)
                return configuration.GetConnectionString("ConnectionMySQLServer")!;
            else
                return configuration.GetConnectionString("ConnectionSQLServer")!;
        }
    }
}
