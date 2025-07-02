using NutriCount.Domain.Entities;

namespace NutriCount.Domain.Services.ServiceBus
{
    public interface IDeleteUserQueue
    {
        Task SendMessage(User user);
    }
}
