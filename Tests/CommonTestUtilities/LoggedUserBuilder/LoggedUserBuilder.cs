using Moq;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUserBuilder
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>();

            mock.Setup(x => x.User()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
