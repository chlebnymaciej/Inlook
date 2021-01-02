using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetContactsPageModel
    {
        /// <summary>
        /// List of contacts.
        /// </summary>
        public IEnumerable<GetUserModel> Contacts { get; set; }
        /// <summary>
        /// Count of all contacts.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
