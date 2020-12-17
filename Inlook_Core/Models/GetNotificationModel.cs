using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetNotificationModel
    {
        /// <summary>
        /// Notification content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// List od notification recepients
        /// </summary>
        public string[] RecipientsList { get; set; }
        /// <summary>
        /// Indicates if notification has attachments
        /// </summary>
        public bool WithAttachments { get; set; }
        /// <summary>
        /// Notification status
        /// </summary>
        public NotificationStatusModel Status { get; set; }
        /// <summary>
        /// Date the notification was send
        /// </summary>
        public string SentAt { get; set; }
        /// <summary>
        /// Date the notification was precessed
        /// </summary>
        public string ProcessAt { get; set; }
    }
}
