using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Requests.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshRequest
    {
        /// <summary>
        /// AppSecret as provided
        /// </summary>
        [Required(ErrorMessage = "Secret must be provided", AllowEmptyStrings = false)]
        public string AppSecret { get; set; }
    }
}
