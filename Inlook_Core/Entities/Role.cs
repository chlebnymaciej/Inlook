using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Role : Base
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

    }
}
