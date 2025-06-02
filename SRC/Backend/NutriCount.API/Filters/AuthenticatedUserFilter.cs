using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using NutriCount.Communication.Responses;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Security.Tokens;
using NutriCount.Exceptions.ExceptionsBase;
using NutriCount.Exceptions;
using NutriCount.Domain.Extensions;

namespace NutriCount.API.Filters
{
    public class AuthenticatedUserFilter: IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;
        public AuthenticatedUserFilter(IAccessTokenValidator acessTokenValidator, IUserReadOnlyRepository repository)
        {
            _accessTokenValidator = acessTokenValidator;
            _repository = repository;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);
                if (exist.IsFalse())
                {
                    throw new UnauthorizedException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
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
            if (string.IsNullOrWhiteSpace(authentication))
            {
                throw new UnauthorizedException(ResourceMessageException.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
