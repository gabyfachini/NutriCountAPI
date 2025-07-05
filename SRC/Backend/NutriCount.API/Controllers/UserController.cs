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
        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="request">Dados do usuário para registro (nome, e-mail, senha, etc.).</param>
        /// <returns>Retorna os dados do usuário criado com status 201.</returns>
        [HttpPost] // Define que este endpoint responde a requisições POST
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)] //O statuscode é o retorno do endpoint quando bem sucedido 
        public async Task <IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase, 
            [FromBody] RequestRegisterUserJson request) //injetados via DI. Função do endpoint, não deveria vir do corpo da requisição? 
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        /// <summary>
        /// Retorna o perfil do usuário autenticado.
        /// </summary>
        /// <returns>Informações de perfil do usuário atual (nome, e-mail, data de registro, etc.).</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza os dados do perfil do usuário autenticado.
        /// </summary>
        /// <param name="request">Novos dados para atualização do usuário.</param>
        /// <returns>Status 204 se a atualização for bem-sucedida.</returns>
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

        /// <summary>
        /// Altera a senha do usuário autenticado.
        /// </summary>
        /// <param name="request">Objeto com senha atual e nova senha.</param>
        /// <returns>Status 204 se a senha for alterada com sucesso.</returns>
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

        /// <summary>
        /// Remove permanentemente o usuário autenticado do sistema.
        /// </summary>
        /// <returns>Status 204 se a exclusão for concluída com sucesso.</returns>
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
