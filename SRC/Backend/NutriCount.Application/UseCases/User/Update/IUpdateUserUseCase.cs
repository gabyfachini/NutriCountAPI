using NutriCount.Communication.Request;

namespace NutriCount.Application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUserJson request);
    }
}
