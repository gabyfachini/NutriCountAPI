using NutriCount.Domain.Security.Cryptography;
using NutriCount.Infrastructure.Secutiry.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new Sha512Encripter("abc1234");
    }
}
