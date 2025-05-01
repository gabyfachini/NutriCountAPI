using Microsoft.AspNetCore.Mvc;
using NutriCount.Application.UseCases.User.Register;
using NutriCount.Communication.Request;
using NutriCount.Communication.Responses;

namespace NutriCount.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
    }
}
