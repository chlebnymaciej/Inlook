using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public int Priority { get; set; }

    }
}
