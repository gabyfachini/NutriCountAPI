using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Services.LoggedUser
{
    public interface ILoggerUser
    {
        public Task<User> User();
    }
}
