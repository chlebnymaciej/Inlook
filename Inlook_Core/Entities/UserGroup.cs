using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Inlook_Core.Entities
{
    public class UserGroup
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Group Group { get; set; }
        public Guid GroupId { get; set; }
    }
}   
