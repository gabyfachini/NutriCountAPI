using Microsoft.AspNetCore.Mvc;
using NutriCount.API.Filters;

namespace NutriCount.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //Attribute that can be used on methods and classes
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) 
        {

        }
    }
}
