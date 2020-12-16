using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetAccountModel
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Indicates if user was accepted
        /// </summary>
        public bool Accepted { get; set; }
    }
}
