using NutriCount.API.Filters;
using NutriCount.API.Middleware;
using NutriCount.Application;
using NutriCount.Infrastructure;
using NutriCount.Infrastructure.Extensions;
using NutriCount.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddRouting(options => options.LowercaseUrls = true); //deixa as URLs min�sculas, que � o padr�o

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<CultureMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    MigrateDatabase();

    app.Run();

    void MigrateDatabase()
    {
        if (builder.Configuration.IsUnitTestEnviroment())
            return;

        var databaseType = builder.Configuration.DatabaseType();
        var connectionString = builder.Configuration.ConnectionString();

        var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        DatabaseMigration.Migrate(databaseType, connectionString, serviceScope.ServiceProvider);
    }
    public partial class Program
    {
        protected Program() { }
    }