using Core.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Requests.Auth
{
    /// <summary>
    /// Object to activate a client
    /// </summary>
    public class ActivationRequest
    {
        /// <summary>
        /// ApiKey as provided when registering
        /// </summary>        
        [Required(ErrorMessage = "apiKey must be provided", AllowEmptyStrings = false)]
        public Guid ApiKey { get; set; }

        /// <summary>
        /// Role to be assigned
        /// </summary>
        [Required(ErrorMessage = "role must be provided", AllowEmptyStrings = false)]
        public AssignableRoles Role { get; set; }
    }
}
