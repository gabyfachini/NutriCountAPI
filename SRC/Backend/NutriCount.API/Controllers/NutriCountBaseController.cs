using Microsoft.AspNetCore.Mvc;

namespace NutriCount.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NutriCountBaseController : ControllerBase
    {
        //Não deveria ter código de não autenticação aqui ?
    }
}
