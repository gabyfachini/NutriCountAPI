using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using NutriCount.Domain.Enums;

namespace NutriCount.Infrastructure.Migrations
{
    public class DatabaseMigration
    {
        public static void Migrate(DatabaseType databaseType, string connectionString, IServiceProvider serviceProvider)
        {
            if (databaseType == DatabaseType.MySql)
                EnsureDatabaseCreated_MySql(connectionString);
            else
                EnsureDatabaseCreated_SqlServer(connectionString);

            MigrationDatabase(serviceProvider);
        }
        private static void EnsureDatabaseCreated_MySql(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.Database;
            connectionStringBuilder.Remove("Database");
            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name",databaseName);
            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);
            
            if (records.Any() == false)
                dbConnection.Execute("CREATE DATABASE {TESTE1}"); //Analisar esse nome de DATABASE
        }
        private static void EnsureDatabaseCreated_SqlServer(string connectionString) //Verifica se o banco existe. Se não existir, cria.
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString); //Quebra a string de conexão para pegar informações específicas (nome do banco, servidor, etc.).
            var databaseName = connectionStringBuilder.InitialCatalog; //Captura o nome do banco de dados (InitialCatalog equivale ao Database na string de conexão).
            connectionStringBuilder.Remove("Database"); //Remove o nome do banco da string de conexão.
            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString); //Abre a conexão com o servidor SQL (sem especificar banco).
            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName); //Prepara os parâmetros da query (usa o nome do banco pra consultar).
            var records = dbConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters); //Executa uma query no SQL Server que verifica se existe um banco com aquele nome. A tabela sys.databases guarda todos os bancos existentes no servidor SQL.

            if (records.Any() == false)
                dbConnection.Execute("CREATE DATABASE [TESTE2]"); //Cria o banco dinamicamente, se não existir

        }
        private static void MigrationDatabase(IServiceProvider serviceProvider) //Executa as migrações pendentes no banco (via FluentMigrator).
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>(); //Pega o serviço de Migration Runner, que é responsável por executar as migrações definidas no FluentMigrator.
            runner.ListMigrations(); // Lista no console todas as migrações disponíveis (só exibe, não executa).
            runner.MigrateUp(); //Executa todas as migrações pendentes.
        }

    }
}
