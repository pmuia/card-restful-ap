using Core.Domain.Utils;

namespace Cards.API.Models.DTOs.Responses.MessagingModule
{
    /// <summary>
    /// 
    /// </summary>
    public class TextAlertDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long TextAlertId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte DlrStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DlrStatusDescription => EnumHelper.GetDescription((DLRStatus)DlrStatus);

        /// <summary>
        /// 
        /// </summary>
        public string Reference { get; set; }

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
        public bool IsQueued { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPosted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object ModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Commands { get; set; }
    }
}
