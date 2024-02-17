using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Cards.API.Models.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionErrorProvider : IErrorResponseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult CreateResponse(ErrorResponseContext context)
        {

            ResponseObject<object> responseObject = new ResponseObject<object>
            {
                Status = new ResponseStatus
                {
                    Code = $"{context.StatusCode}",
                    Message = $"{context.ErrorCode} - {context.Message}"
                }
            };

            return new BadRequestObjectResult(responseObject);
        }
    }
}
