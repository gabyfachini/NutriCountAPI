﻿using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Repositories.Token;
using NutriCount.Infrastructure.DataAcess;

namespace NutriCount.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly NutriCountDbContext _dbContext;

        public TokenRepository(NutriCountDbContext dbContext) => _dbContext = dbContext;

        public async Task<RefreshToken?> Get(string refreshToken)
        {
            return await _dbContext
                .RefreshTokens
                .AsNoTracking()
                .Include(token => token.User)
                .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
        }

        public async Task SaveNewRefreshToken(RefreshToken refreshToken)
        {
            var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

            _dbContext.RefreshTokens.RemoveRange(tokens);

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
        }
    }
}
