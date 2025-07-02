using Microsoft.AspNetCore.Mvc;
using NutriCount.API.Attributes;
using NutriCount.Application.UseCases.User.Delete.Request;
using NutriCount.Application.UseCases.User.Profile;
using NutriCount.Application.UseCases.User.Register;
using NutriCount.Application.UseCases.User.Update;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.API.Controllers
{
    [AuthenticatedUser]
    public class UserController : NutriCountBaseController
    {
        [HttpPost] // Define que este endpoint responde a requisições POST
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)] //O statuscode é o retorno do endpoint quando bem sucedido 
        public async Task <IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase, 
            [FromServices] RequestRegisterUserJson request) //injetados via DI. Função do endpoint, não deveria vir do corpo da requisição? 
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }
        [HttpPut("change-password")] //Informação extra da senha porque não pode ter endpoint repetido!
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AuthenticatedUser]
        public async Task<IActionResult> Delete([FromServices] IRequestDeleteUserUseCase useCase)
        {
            await useCase.Execute();

            return NoContent();
        }
    }
}
