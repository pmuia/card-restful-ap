using Core.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Cards.API.Models.Common;
using System.Net;

namespace Cards.API.Models.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ExceptionFilter> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public ExceptionFilter(IWebHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception.GetType() == typeof(GenericException))
            {
                GenericException genericException = context.Exception as GenericException;

                ResponseObject<object> responseObject = new ResponseObject<object>
                {
                    Status = new ResponseStatus { Code = $"{(int)genericException.StatusCode}", Message = genericException.UserMessage }
                };

                context.Result = new HttpActionResult(responseObject, (int)genericException.StatusCode);
                context.HttpContext.Response.StatusCode = (int)genericException.StatusCode;
            }
            else
            {
                string genericMessage = "Sorry, your request could not be competed. If problem persists, please contact us for assistance";
                if (env.IsDevelopment())
                {
                    genericMessage = $"{context.Exception.Message} | {context.Exception.StackTrace}";
                }

                ResponseObject<object> responseObject = new ResponseObject<object>
                {
                    Status = new ResponseStatus { Code = $"{(int)HttpStatusCode.InternalServerError}", Message = genericMessage }
                };

                context.Result = new HttpActionResult(responseObject, (int)HttpStatusCode.InternalServerError);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}
