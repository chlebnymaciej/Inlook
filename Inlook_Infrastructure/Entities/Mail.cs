using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Entities
{
    public class Mail
    {
        public Guid Id { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public virtual ICollection<MailTo> Recipients { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
