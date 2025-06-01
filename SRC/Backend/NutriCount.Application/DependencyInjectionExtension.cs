using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutriCount.Application.Services.AutoMapper;
using NutriCount.Application.Services.Cryptography;
using NutriCount.Application.UseCases.Login.DoLogin;
using NutriCount.Application.UseCases.User.Profile;
using NutriCount.Application.UseCases.User.Register;

namespace NutriCount.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncripter(services, configuration);
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
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        }
        private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
