namespace Cards.API.Models.DTOs.Responses.AdminModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }

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
