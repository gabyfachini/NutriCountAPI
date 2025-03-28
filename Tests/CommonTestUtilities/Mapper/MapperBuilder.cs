using AutoMapper;
using NutriCount.Application.Services.AutoMapper;

namespace UseCases.Test.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            return new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

        }
    }
}