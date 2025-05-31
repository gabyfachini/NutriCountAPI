using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Exceptions;
using NutriCount.Exceptions.ExceptionsBase;

namespace NutriCount.API.Attributes
{
    public class AuthenticatedUserAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _acessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;
        public AuthenticatedUserAttribute(IAccessTokenValidator acessTokenValidator, IUserReadOnlyRepository repository)
        {
            _acessTokenValidator = acessTokenValidator;
            _repository = repository;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);
                var userIdentifier = _acessTokenValidator.ValidateAndGetUserIdentifier(token);
                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);
                if (exist == false)
                {
                    throw new NutriCountException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token is Expired")
                {
                    TokenIsExpired = true,
                });
            }
            catch (NutriCountException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch 
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
            }
        }
        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authentication))
            {
                throw new NutriCountException(ResourceMessageException.NO_TOKEN);
            }
            return authentication["Bearer ".Length..].Trim();
        }

    }
}
