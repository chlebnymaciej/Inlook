using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Attachment : Base
    {
        public Guid Id { get; set; }
        public Mail Mail { get; set; }
        public Guid MailId { get; set; }
        public string FilePath { get; set; }
    }
}
