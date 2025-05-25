using NutriCount.Application.Services.Cryptography;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(
            IUserReadOnlyRepository repository,
            IAccessTokenGenerator accessTokenGenerator,
            PasswordEncripter passwordEncripter)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AcessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }

        private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
        {
            var refreshToken = new Domain.Entities.RefreshToken
            {
                Value = _refreshTokenGenerator.Generate(),
                UserId = user.Id
            };

            await _tokenRepository.SaveNewRefreshToken(refreshToken);

            await _unitOfWork.Commit();

            return refreshToken.Value;
        }
    }
}
