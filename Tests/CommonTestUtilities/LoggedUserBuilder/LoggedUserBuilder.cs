using Moq;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUserBuilder
{
    public class LoggedUserBuilder
    {
        public static ILoggerUser Build(User user)
        {
            var mock = new Mock<ILoggerUser>();

            mock.Setup(x => x.User()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
