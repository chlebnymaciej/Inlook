using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class User : Base
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public virtual ICollection<Favorites> FavoritesUsers { get; set; } = new HashSet<Favorites>();
        public virtual ICollection<Favorites> UsersThatFavorize { get; set; } = new HashSet<Favorites>();
        public virtual ICollection<Mail> MailsSend { get; set; } = new HashSet<Mail>();
        public virtual ICollection<MailTo> MailsReceived { get; set; } = new HashSet<MailTo>();
        public virtual ICollection<UserGroup> UserGroups { get; set; } = new HashSet<UserGroup>();
        public virtual ICollection<Group> GroupsOwned { get; set; } = new HashSet<Group>();
    }
}   
