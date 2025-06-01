using Microsoft.AspNetCore.Mvc;
using NutriCount.API.Filters;

namespace NutriCount.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) 
        {

        }
    }
}
