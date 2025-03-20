using Microsoft.Extensions.DependencyInjection;
using NutriCount.Application.Services.AutoMapper;
using NutriCount.Application.Services.Cryptography;
using NutriCount.Application.UseCases.User.Register;
using System.Data;

namespace NutriCount.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddPasswordEncripter(services);
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }
        private static void AddPasswordEncripter(IServiceCollection services)
        {
            services.AddScoped(option => new PasswordEncripter());
        }
    }
}
