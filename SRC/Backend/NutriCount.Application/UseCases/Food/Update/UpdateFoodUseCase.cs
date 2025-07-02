using NutriCount.Communication.Request;
using NutriCount.Domain.Repositories.Food;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Entities;

namespace NutriCount.Application.UseCases.Food.Update
{
    public class UpdateFoodUseCase : IUpdateFoodUseCase
    {
        private readonly IFoodReadOnlyRepository _repositoryRead;
        private readonly IFoodWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFoodUseCase(
            IFoodReadOnlyRepository repositoryRead,
            IFoodWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Execute(int id, RequestFoodUpdateJson request)
        {
            var food = await _repositoryRead.GetByIdAsync(id);

            if (food is null)
                return false;

            food.Name = request.Name;
            food.ServingSize = request.ServingSize;
            food.Calories = request.Calories;
            food.Proteins = request.Proteins;
            food.Carbohydrates = request.Carbohydrates;
            food.TotalFats = request.TotalFats;
            food.SaturatedFats = request.SaturatedFats;
            food.TransFats = request.TransFats;
            food.DietaryFiber = request.DietaryFiber;
            food.Sodium = request.Sodium;
            food.Calcium = request.Calcium;
            food.Iron = request.Iron;
            food.VitaminA = request.VitaminA;
            food.VitaminC = request.VitaminC;
            food.Potassium = request.Potassium;
            food.Magnesium = request.Magnesium;
            food.Zinc = request.Zinc;
            food.VitaminD = request.VitaminD;
            food.VitaminE = request.VitaminE;
            food.FolicAcid = request.FolicAcid;
            food.Choline = request.Choline;
            food.Phosphorus = request.Phosphorus;
            food.DataSource = request.DataSource;

            await _repositoryWrite.UpdateAsync(food);
            await _unitOfWork.Commit();

            return true;
        }
    }
}
