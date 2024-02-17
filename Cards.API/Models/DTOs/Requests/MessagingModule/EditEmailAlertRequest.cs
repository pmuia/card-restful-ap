namespace Cards.API.Models.DTOs.Requests.MessagingModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EditEmailAlertRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailAlertId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte DlrStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Origin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte SendRetry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SecurityCritical { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasAttachment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AttachmentFilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsQueued { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPosted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
