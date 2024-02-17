using Core.Domain.Common;

namespace Core.Domain.Entities.CardModule.Aggregates
{
    public class Card : AuditableEntity
    {
        public long CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
