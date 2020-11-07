using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Group : Base
    {
        public Guid Id { get; set; }
        public User GroupOwner { get; set; }
        public Guid GroupOwnerId { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new HashSet<UserGroup>();
    }
}   
