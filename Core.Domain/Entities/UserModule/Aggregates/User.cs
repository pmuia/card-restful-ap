

using Core.Domain.Common;

namespace Core.Domain.Entities.UserModule.Aggregates
{
    public class User : AuditableEntity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public byte Role { get; set; }
        public string Password { get; set; }
    }
}
