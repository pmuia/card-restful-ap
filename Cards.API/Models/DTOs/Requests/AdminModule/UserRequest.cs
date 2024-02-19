namespace Cards.API.Models.DTOs.Requests.AdminModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRequest
    {
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
