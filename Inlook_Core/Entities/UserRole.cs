using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class UserRole
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
