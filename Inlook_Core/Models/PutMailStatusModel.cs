using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    public class PutMailStatusModel
    {
        public Guid MailId { get; set; }
        public bool Read { get; set; }
    }
}
