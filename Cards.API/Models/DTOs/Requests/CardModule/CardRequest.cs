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
