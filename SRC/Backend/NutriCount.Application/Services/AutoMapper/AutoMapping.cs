using AutoMapper;
using NutriCount.Communication.Request;

namespace NutriCount.Application.Services.AutoMapper
{
    public class AutoMapping : Profile 
    {
        public AutoMapping() 
        {
            RequestToDomain();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>() //situação da criptografia
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }

}
