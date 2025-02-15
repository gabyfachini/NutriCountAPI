using Microsoft.AspNetCore.Http;
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
        [HttpPost] //o tipo do endpoint
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Regiter(RequestRegisterUserJson request) //função do endpoint
        {
            var useCase = new RegisterUserUseCase();
            var result = useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
