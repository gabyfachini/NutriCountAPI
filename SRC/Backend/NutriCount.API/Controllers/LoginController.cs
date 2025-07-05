using Microsoft.AspNetCore.Mvc;
using NutriCount.Application.UseCases.Login.DoLogin;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : NutriCountBaseController
    {
        /// <summary>
        /// Realiza o login do usuário com base nas credenciais fornecidas.
        /// </summary>
        /// <param name="request">Objeto contendo e-mail e senha do usuário.</param>
        /// <returns>
        /// Retorna os dados do usuário autenticado e token de acesso caso o login seja bem-sucedido.
        /// Retorna erro 401 caso as credenciais estejam incorretas.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
