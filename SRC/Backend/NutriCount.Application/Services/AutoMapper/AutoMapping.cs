using AutoMapper;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.Application.Services.AutoMapper
{
    public class AutoMapping : Profile 
    {
        public AutoMapping() 
        {
            RequestToDomain();
            DomainToResponse();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>() //situação da criptografia
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
        }
    }

}
