using NutriCount.Communication.Responses;

namespace NutriCount.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}
