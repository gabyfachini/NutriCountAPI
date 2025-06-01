﻿using NutriCount.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Repositories.User;

namespace NutriCount.Infrastructure.DataAcess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly NutriCountDbContext _dbContext;
        public UserRepository(NutriCountDbContext dbContext) => _dbContext = dbContext;
        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
        public async Task<User> GetByUserIdentifier(Guid userIdentifier)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.UserIdentifier.Equals(userIdentifier));
        }
        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }
        public async Task<User> GetById(long id)
        {
            return await _dbContext
                .Users
                .FirstAsync(user => user.Id == id);
        }
        public void Update(User user) => _dbContext.Users.Update(user);
    }
}
