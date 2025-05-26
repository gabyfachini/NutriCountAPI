using NutriCount.Domain.Security.Tokens;

namespace NutriCount.Infrastructure.Secutiry.Tokens.Refresh
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
