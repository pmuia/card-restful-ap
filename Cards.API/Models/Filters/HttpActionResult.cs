using Microsoft.AspNetCore.Mvc;

namespace Cards.API.Models.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpActionResult : IActionResult
    {
        private readonly object message;
        private readonly int statusCode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public HttpActionResult(object message, int statusCode)
        {
            this.message = message;
            this.statusCode = statusCode;
        }

        async Task IActionResult.ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult = new ObjectResult(message)
            {
                StatusCode = statusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
