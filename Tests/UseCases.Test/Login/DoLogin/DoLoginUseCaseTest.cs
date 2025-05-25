using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using NutriCount.Application.UseCases.Login.DoLogin;
using NutriCount.Communication.Request;
using NutriCount.Exceptions.ExceptionsBase;
using NutriCount.Exceptions;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Entities;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = password
            });

            result.Should().NotBeNull();
            result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
        }

        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            Func<Task> act = async () => { await useCase.Execute(request); };

            await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(e => e.Message.Equals(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID));
        }

        private static DoLoginUseCase CreateUseCase(NutriCount.Domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (user is not null)
                userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);

            return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(), passwordEncripter);
        }
    }
}
