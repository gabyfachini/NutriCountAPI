using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using NutriCount.Application.UseCases.User.Register;
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
            result.Name.Should().Be(request.Name);
        }
        private RegisterUserUseCase CreateUseCase()
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder();

            return new RegisterUserUseCase(writeRepository,
                                                  readRepository, 
                                                  unitOfWork,
                                                  passwordEncripter,
                                                  mapper);
        }
    }
}
