using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Requests.AdminModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EditUserRequest
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId must be provided")]
        [Range(1, long.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
