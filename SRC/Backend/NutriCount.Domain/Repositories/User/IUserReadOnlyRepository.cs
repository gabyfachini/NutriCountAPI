﻿namespace NutriCount.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    }
}
