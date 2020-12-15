using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Models
{
    public class GetAccountModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }

        public bool Accepted { get; set; }
    }
}
