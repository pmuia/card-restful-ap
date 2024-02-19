using Core.Domain.Common;
using Core.Domain.Entities.UserModule.Aggregates;

namespace Core.Domain.Entities.CardModule.Aggregates
{
    public class Card : AuditableEntity
    {
        public long CardId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public User User { get; set; }
    }
}
