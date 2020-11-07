using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Mail : Base
    {
        public Guid Id { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public virtual ICollection<MailTo> Recipients { get; set; } = new HashSet<MailTo>();
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
