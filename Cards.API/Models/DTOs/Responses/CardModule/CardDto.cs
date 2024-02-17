using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models.DTOs.Responses.CardModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// unique identifier
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        public string? CreatedBy { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
