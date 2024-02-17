using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Cards.API.Models.Common;
using System.Net;

namespace Cards.API.Models.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelStateFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                ResponseObject<object> responseObject = new ResponseObject<object>
                {
                    Status = new ResponseStatus
                    {
                        Code = $"{(int)HttpStatusCode.BadRequest}",
                        Message = string.Join(" | ", context.ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage))
                    }
                };

                context.Result = new BadRequestObjectResult(responseObject);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
