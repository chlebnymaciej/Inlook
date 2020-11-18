using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class MailTo 
    {
        public Mail Mail { get; set; }
        public Guid MailId { get; set; }
        public User Recipient { get; set; }
        public Guid RecipientId { get; set; }
        /*
         * CC value information:
         * - True - CC
         * - False - BCC
         * - NULL - normal message
         */
        public bool? CC { get; set; }
        public bool StatusRead { get; set; }
    }
}
