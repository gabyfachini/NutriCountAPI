using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Entities;
using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.Food;

namespace NutriCount.Application.UseCases.Food.Register
{
    public class RegisterFoodUseCase : IRegisterFoodUseCase
    {
        private readonly IFoodWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterFoodUseCase(
            IFoodWriteOnlyRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseFoodJson> Execute(RequestFoodRegisterJson request)
        {
            var food = new Eating
            {
                Name = request.Name,
                ServingSize = request.ServingSize,
                Calories = request.Calories,
                Proteins = request.Proteins,
                Carbohydrates = request.Carbohydrates,
                TotalFats = request.TotalFats,
                SaturatedFats = request.SaturatedFats,
                TransFats = request.TransFats,
                DietaryFiber = request.DietaryFiber,
                Sodium = request.Sodium,
                Calcium = request.Calcium,
                Iron = request.Iron,
                VitaminA = request.VitaminA,
                VitaminC = request.VitaminC,
                Potassium = request.Potassium,
                Magnesium = request.Magnesium,
                Zinc = request.Zinc,
                VitaminD = request.VitaminD,
                VitaminE = request.VitaminE,
                FolicAcid = request.FolicAcid,
                Choline = request.Choline,
                Phosphorus = request.Phosphorus,
                DataSource = request.DataSource
            };

            await _repository.AddAsync(food);
            await _unitOfWork.Commit();

            return new ResponseFoodJson
            {
                Id = food.Id,
                Name = food.Name,
                ServingSize = food.ServingSize,
                Calories = food.Calories,
                Proteins = food.Proteins,
                Carbohydrates = food.Carbohydrates,
                TotalFats = food.TotalFats,
                SaturatedFats = food.SaturatedFats,
                TransFats = food.TransFats,
                DietaryFiber = food.DietaryFiber,
                Sodium = food.Sodium,
                Calcium = food.Calcium,
                Iron = food.Iron,
                VitaminA = food.VitaminA,
                VitaminC = food.VitaminC,
                Potassium = food.Potassium,
                Magnesium = food.Magnesium,
                Zinc = food.Zinc,
                VitaminD = food.VitaminD,
                VitaminE = food.VitaminE,
                FolicAcid = food.FolicAcid,
                Choline = food.Choline,
                Phosphorus = food.Phosphorus,
                DataSource = food.DataSource
            };
        }
    }
}
