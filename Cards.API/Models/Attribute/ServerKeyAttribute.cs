using Core.Management.Interfaces.AdminModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Cards.API.Models.Common;
using System.Net;

namespace Cards.API.Models.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ServerKeyAttribute : System.Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("x-server-key", out StringValues serverKey))
            {
                ResponseObject<object> responseObject = new ResponseObject<object>
                {
                    Status = new ResponseStatus
                    {
                        Code = $"{(int)HttpStatusCode.Unauthorized}",
                        Message = "x-server-key header is missing"
                    }
                };

                context.Result = new UnauthorizedObjectResult(responseObject);

                return;
            }

            ISecurityRepository repository = context.HttpContext.RequestServices.GetRequiredService<ISecurityRepository>();
            bool isValid = repository.ValidateServerKey(serverKey.FirstOrDefault()?.Trim());

            if (!isValid)
            {
                ResponseObject<object> responseObject = new ResponseObject<object>
                {
                    Status = new ResponseStatus
                    {
                        Code = $"{(int)HttpStatusCode.Unauthorized}",
                        Message = "x-server-key is not valid"
                    }
                };

                context.Result = new UnauthorizedObjectResult(responseObject);
                return;
            }

            await next();
        }
    }
}

