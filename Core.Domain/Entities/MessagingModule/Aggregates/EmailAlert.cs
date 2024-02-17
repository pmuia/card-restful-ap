

using Core.Domain.Common;

namespace Core.Domain.Entities.MessagingModule.Aggregates
{
    public class EmailAlert : AuditableEntity
    {
        public long EmailAlertId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public byte DlrStatus { get; set; }
        public byte Origin { get; set; }
        public byte Priority { get; set; }
        public byte SendRetry { get; set; }
        public bool SecurityCritical { get; set; }
        public bool HasAttachment { get; set; }
        public string AttachmentFilePath { get; set; }
        public bool IsQueued { get; set; }
        public bool IsPosted { get; set; }
    }
}
