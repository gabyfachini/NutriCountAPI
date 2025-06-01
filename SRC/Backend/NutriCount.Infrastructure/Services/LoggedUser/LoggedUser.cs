using Microsoft.EntityFrameworkCore;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Domain.Services.LoggedUser;
using NutriCount.Infrastructure.DataAcess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NutriCount.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggerUser
    {
        private readonly NutriCountDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(NutriCountDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
