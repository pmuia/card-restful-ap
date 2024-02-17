

using Core.Domain.Common;

namespace Core.Domain.Entities.MessagingModule.Aggregates
{
    public class TextAlert : AuditableEntity
    {
        public long TextAlertId { get; set; }
        public string Recipient { get; set; }
        public string Body { get; set; }
        public byte DlrStatus { get; set; }
        public string Reference { get; set; }
        public byte Origin { get; set; }
        public byte Priority { get; set; }
        public byte SendRetry { get; set; }
        public bool SecurityCritical { get; set; }
        public bool IsQueued { get; set; }
        public bool IsPosted { get; set; }
    }
}
