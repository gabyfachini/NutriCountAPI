using AutoMapper;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Services.LoggedUser;

namespace NutriCount.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggerUser _loggedUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggerUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();

            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
