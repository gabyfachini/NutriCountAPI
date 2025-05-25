using Microsoft.OpenApi.Models;
using NutriCount.API.Filters;
using NutriCount.API.Middleware;
using NutriCount.Application;
using NutriCount.Infrastructure;
using NutriCount.Infrastructure.Extensions;
using NutriCount.Infrastructure.Migrations;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new StringConverter());
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme.
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityRequirement
         {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    });

    builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddRouting(options => options.LowercaseUrls = true); //deixa as URLs minúsculas, que é o padrão

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