using AutoMapper;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using Sqids;

namespace NutriCount.Application.Services.AutoMapper
{
    public class AutoMapping : Profile 
    {
        private readonly SqidsEncoder<long> _idEnconder;
        public AutoMapping(SqidsEncoder<long> idEnconder) 
        {
            _idEnconder = idEnconder;

            RequestToDomain();
            DomainToResponse();
        }
        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<Domain.Entities.Foods, ResponseFoodJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)));
        }
    }

}
