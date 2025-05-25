using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using FluentAssertions.Equivalency.Steps;
using NutriCount.Application.UseCases.User.Register;
using NutriCount.Exceptions;
using NutriCount.Exceptions.ExceptionsBase;
using UseCases.Test.Mapper;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Tokens.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Alreaady_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase(request.Email);
            Func<Task> act = async () => await useCase.Execute(request);
            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessageException.EMAIL_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;


            var useCase = CreateUseCase();
            Func<Task> act = async () => await useCase.Execute(request);
            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessageException.NAME_EMPTY));
        }
        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var acessTokenGenerator = JwtTokenGeneratorBuilder.Build();

            if (string.IsNullOrEmpty(email) == false)
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(writeRepository,
                                                  readRepositoryBuilder.Build(), 
                                                  unitOfWork,
                                                  passwordEncripter,
                                                  acessTokenGenerator,
                                                  mapper);
        }
    }
}
