using NutriCount.Domain.Security.Tokens;
using NutriCount.Infrastructure.Secutiry.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build () => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "tttttttttttttttttttttttttttttttttt");
    }
}
