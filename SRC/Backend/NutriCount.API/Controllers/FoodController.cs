using Microsoft.AspNetCore.Mvc;
using NutriCount.Application.UseCases.Food.Delete;
using NutriCount.Application.UseCases.Food.GetAll;
using NutriCount.Application.UseCases.Food.GetById;
using NutriCount.Application.UseCases.Food.Register;
using NutriCount.Application.UseCases.Food.Update;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : NutriCountBaseController
    {
        /// <summary>
        /// Registra um novo alimento no banco de dados.
        /// </summary>
        /// <param name="request">Coloque os dados do alimento a ser criado.</param>
        /// <returns>O alimento foi registrado com seu ID.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseFoodJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterFoodUseCase useCase,
            [FromBody] RequestFoodRegisterJson request)
        {
            var result = await useCase.Execute(request); 
            return Created(string.Empty, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseFoodJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetFoodByIdUseCase useCase, 
            [FromRoute] int id)
        {
            var result = await useCase.Execute(id); 

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResponseFoodJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllFoodsUseCase useCase) 
        {
            var result = await useCase.Execute(); 
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateFoodUseCase useCase,
            [FromRoute] int id,
            [FromBody] RequestFoodUpdateJson request)
        {
            var success = await useCase.Execute(id, request); 

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteFoodUseCase useCase, 
            [FromRoute] int id)
        {
            var success = await useCase.Execute(id); 

            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
