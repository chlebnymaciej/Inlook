using Inlook_Core.Models.Attachments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    public class GetMailModel
    {
        /// <summary>
        /// Mail sender
        /// </summary>
        public GetUserWithIdModel From { get; set; }
        /// <summary>
        /// Mail recepients
        /// </summary>
        public GetUserWithIdModel[] To { get; set; }
        /// <summary>
        /// Mail CC recepients
        /// </summary>
        public GetUserWithIdModel[] CC { get; set; }
        /// <summary>
        /// Attachments on mail
        /// </summary>
        public GetAttachmentModel[] Attachments { get; set; }
        /// <summary>
        /// Mail Id
        /// </summary>
        public Guid MailId { get; set; }
        /// <summary>
        /// Indicates if mail was read
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
        /// Mail subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Mail content text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Date the mail was send
        /// </summary>
        public DateTime SendTime { get; set; }
    }
}
