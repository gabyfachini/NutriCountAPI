using Microsoft.OpenApi.Models;
using NutriCount.API.Filters;
using NutriCount.API.Middleware;
using NutriCount.API.Token;
using NutriCount.Application;
using NutriCount.Domain.Repositories.Token;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Infrastructure;
using NutriCount.Infrastructure.Extensions;
using NutriCount.Infrastructure.Migrations;
using NutriCount.Infrastructure.Repositories;
using NutriCount.Infrastructure.Secutiry.Tokens.Refresh;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}); //Habilitando comentários XML na Swagger

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
        options.AddSecurityRequirement( new OpenApiSecurityRequirement
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
    builder.Services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    builder.Services.AddScoped<ITokenRepository, TokenRepository>();
    builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

    builder.Services.AddRouting(options => options.LowercaseUrls = true); //deixa as URLs minúsculas, que é o padrão
    builder.Services.AddHttpContextAccessor();

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