using Core.Domain.Utils;

namespace Cards.API.Models.DTOs.Responses.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientDto : MinifiedClientDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccessTokenLifetimeInMins { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AuthorizationCodeLifetimeInMins { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
