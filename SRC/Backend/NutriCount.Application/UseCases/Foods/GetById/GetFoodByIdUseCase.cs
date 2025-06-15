using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories.Food;

namespace NutriCount.Application.UseCases.Food.GetById
{
    public class GetFoodByIdUseCase : IGetFoodByIdUseCase
    {
        private readonly IFoodReadOnlyRepository _repository;

        public GetFoodByIdUseCase(IFoodReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseFoodJson?> Execute(int id)
        {
            var food = await _repository.GetByIdAsync(id);

            if (food is null)
                return null;

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
