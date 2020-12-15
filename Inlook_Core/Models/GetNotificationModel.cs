using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetNotificationModel
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string[] RecipientsList { get; set; }
        public bool WithAttachments { get; set; }
        public NotificationStatusModel Status { get; set; }
        public string SentAt { get; set; }
        public string ProcessAt { get; set; }
    }
}
