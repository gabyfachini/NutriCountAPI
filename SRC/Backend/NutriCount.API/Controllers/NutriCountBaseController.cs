using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NutriCount.Domain.Extensions;

namespace NutriCount.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NutriCountBaseController : ControllerBase
    {
        protected static bool IsNotAuthenticated(AuthenticateResult authenticate)
        {
            return authenticate.Succeeded.IsFalse()
                || authenticate.Principal is null
                || authenticate.Principal.Identities.Any(id => id.IsAuthenticated).IsFalse();
        }
    }
}
