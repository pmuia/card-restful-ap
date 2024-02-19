using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Requests.CardModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EditCardRequest
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardId must be provided")]
        [Range(1, long.MaxValue, ErrorMessage = "CardId must be greater than 0")]
        public string CardId { get; set; }

        /// <summary>
        /// unique identifier
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId must be provided")]
        [Range(1, long.MaxValue, ErrorMessage = "CardId must be greater than 0")]
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Record Status
        /// </summary>
        [Display(Name = "Record Status")]
        public byte RecordStatus { get; set; }

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
