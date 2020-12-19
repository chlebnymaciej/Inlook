using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    /// <summary>
    /// Model for editing mail status
    /// </summary>
    public class PutMailStatusModel
    {
        /// <summary>
        /// Id of mail to edit
        /// </summary>
        public Guid MailId { get; set; }
        /// <summary>
        /// New read status
        /// </summary>
        public bool Read { get; set; }
    }
}
