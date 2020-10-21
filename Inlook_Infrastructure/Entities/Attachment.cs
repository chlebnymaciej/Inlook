using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public Mail Mail { get; set; }
        public Guid MailId { get; set; }
        public string FilePath { get; set; }
    }
}
