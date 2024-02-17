using System.Diagnostics;

namespace Cards.API.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestLoggingMiddleware
    {
        // Name of the Response Header, Custom Headers starts with "x-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "x-response-time-ms";
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch  
            Stopwatch watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                // Stop the timer information and calculate the time   
                watch.Stop();
                long responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });

            // Call the next delegate/middleware in the pipeline   
            await _next(context);
        }
    }
}
