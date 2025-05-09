﻿using NutriCount.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Repositories.User;

namespace NutriCount.Infrastructure.DataAcess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly NutriCountDbContext _dbContext;
        public UserRepository(NutriCountDbContext dbContext) => _dbContext = dbContext;
        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    }
}
