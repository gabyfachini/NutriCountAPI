using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.Token;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Cryptography;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IUserReadOnlyRepository userReadOnlyRepository;
        private IAccessTokenGenerator acessTokenGenerator;
        private IPasswordEncripter passwordEncripter;

        public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, IAccessTokenGenerator acessTokenGenerator, IPasswordEncripter passwordEncripter)
        {
            this.userReadOnlyRepository = userReadOnlyRepository;
            this.acessTokenGenerator = acessTokenGenerator;
            this.passwordEncripter = passwordEncripter;
        }

        public DoLoginUseCase(
            IUserReadOnlyRepository repository,
            IAccessTokenGenerator accessTokenGenerator,
            IPasswordEncripter passwordEncripter,
            IRefreshTokenGenerator refreshTokenGenerator,
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
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
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
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
