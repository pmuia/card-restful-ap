using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Requests.CardModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CardRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public string Name { get; set; }

        /// <summary>
        /// unique identifier
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId must be provided")]
        [Range(1, long.MaxValue, ErrorMessage = "CardId must be greater than 0")]
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
