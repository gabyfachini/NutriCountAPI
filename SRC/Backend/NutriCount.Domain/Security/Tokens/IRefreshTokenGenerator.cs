namespace NutriCount.Domain.Security.Tokens
{
    public interface IRefreshTokenGenerator
    {
        public string Generate();
    }
}
