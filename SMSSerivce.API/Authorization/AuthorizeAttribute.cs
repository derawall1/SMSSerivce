

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SMSSerivce.API.Dtos;
using SMSService.Data;

namespace SMSSerivce.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (Account)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in - return 403 forbidden
                var response = new ResponseDto();
                response.Message = "";
                response.Error = "Authentication failed";
                context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status403Forbidden };

                // set 'WWW-Authenticate' header to trigger login popup in browsers
                context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            }
        }
    }
}