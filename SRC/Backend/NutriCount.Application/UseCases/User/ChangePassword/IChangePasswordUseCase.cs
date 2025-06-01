using NutriCount.Communication.Request;

namespace NutriCount.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePasswordJson request);
    }
}
