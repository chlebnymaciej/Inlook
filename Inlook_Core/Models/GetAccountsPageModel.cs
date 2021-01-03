using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetAccountsPageModel
    {
        /// <summary>
        /// List of accounts.
        /// </summary>
        public IEnumerable<GetAccountModel> Accounts { get; set; }
        /// <summary>
        /// Count of all accounts.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
