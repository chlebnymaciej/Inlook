using Inlook_Core.Models.Attachments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    public class GetMailModel
    {
        public GetUserWithIdModel From { get; set; }
        public GetUserWithIdModel[] To { get; set; }
        public GetUserWithIdModel[] CC { get; set; }
        public GetAttachmentModel[] Attachments { get; set; }
        public Guid MailId { get; set; }
        public bool Read { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }
    }
}
