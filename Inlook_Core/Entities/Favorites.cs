using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Favorites
    {
        public User User { get; set; }

        public Guid UserId { get; set; }

        public User FavoriteUser { get; set; }

        public Guid FavoriteUserId { get; set; }
    }
}
