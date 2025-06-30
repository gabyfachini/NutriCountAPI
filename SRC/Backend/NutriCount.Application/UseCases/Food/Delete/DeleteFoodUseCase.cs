using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.Food;

namespace NutriCount.Application.UseCases.Food.Delete
{
    public class DeleteFoodUseCase : IDeleteFoodUseCase
    {
        private readonly IFoodReadOnlyRepository _repositoryRead;
        private readonly IFoodWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFoodUseCase(
            IFoodReadOnlyRepository repositoryRead,
            IFoodWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Execute(int id)
        {
            var food = await _repositoryRead.GetByIdAsync(id);

            if (food is null)
                return false;

            await _repositoryWrite.DeleteAsync(id);

            await _unitOfWork.Commit();

            return true;
        }
    }
}
