using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
