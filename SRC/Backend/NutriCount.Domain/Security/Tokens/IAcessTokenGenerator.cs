﻿namespace NutriCount.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier);
    }
}
