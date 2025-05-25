using AutoMapper;
using NutriCount.Application.Services.Cryptography;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Exceptions;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly PasswordEncripter _passwordEncripter;
        private IUserWriteOnlyRepository writeRepository;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository, 
            IUserReadOnlyRepository readOnlyRepository,
            IUnitOfWork unitOfWork,
            PasswordEncripter passwordEncripter,
            IAccessTokenGenerator accessTokenGenerator,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _writeOnlyRepository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
                Tokens = new ResponseTokensJson
                {
                    AcessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageException.EMAIL_ALREADY_REGISTERED));

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
