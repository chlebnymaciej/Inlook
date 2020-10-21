using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Favorites> FavoritesUsers { get; set; }
        public virtual ICollection<Favorites> UsersThatFavorize { get; set; }
        public virtual ICollection<Mail> MailsSend { get; set; }
        public virtual ICollection<MailTo> MailsReceived { get; set; }
    }
}   
