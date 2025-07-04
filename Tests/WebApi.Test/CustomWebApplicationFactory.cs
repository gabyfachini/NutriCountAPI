﻿using CommonTestUtilities.Entities;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NutriCount.Domain.Entities;
using NutriCount.Infrastructure.DataAcess;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        /*private NutriCount.Domain.Entities.Recipe _recipe = default!;*/
        private NutriCount.Domain.Entities.User _user = default!;
        /*private NutriCount.Domain.Entities.RefreshToken _refreshToken = default!;*/
        private string _password = string.Empty;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                 {
                     var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<NutriCountDbContext>));
                     if (descriptor is not null)
                            services.Remove(descriptor);

                     var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                     services.AddDbContext<NutriCountDbContext>(options =>
                     {
                         options.UseInMemoryDatabase("InMemoryDbForTesting");
                         options.UseInternalServiceProvider(provider);
                     });

                     using var scope = Services.BuildServiceProvider().CreateScope();

                     var dbContext = scope.ServiceProvider.GetRequiredService<NutriCountDbContext>();

                     dbContext.Database.EnsureDeleted();
                     StartDatabase(dbContext);
                 });
        }

        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;
        public Guid GetUserIdentifier() => _user.UserIdentifier;
        
        /*public string GetName() => _user.Name;
        public string GetRefreshToken() => _refreshToken.Value;
        public Guid GetUserIdentifier() => _user.UserIdentifier;

        public string GetRecipeId() => IdEncripterBuilder.Build().Encode(_recipe.Id);
        public string GetRecipeTitle() => _recipe.Title;
        public Difficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
        public CookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
        public IList<DishType> GetDishTypes() => _recipe.DishTypes.Select(c => c.Type).ToList();*/
        private void StartDatabase(NutriCountDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();

            dbContext.Users.Add(_user);

            dbContext.SaveChanges();
        }

    }
}
