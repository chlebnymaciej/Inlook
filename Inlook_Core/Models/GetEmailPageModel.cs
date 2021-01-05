using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetEmailPageModel
    {
        /// <summary>
        /// List of email.
        /// </summary>
        public IEnumerable<GetMailModel> Mails { get; set; }
        /// <summary>
        /// Count of all emails.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
