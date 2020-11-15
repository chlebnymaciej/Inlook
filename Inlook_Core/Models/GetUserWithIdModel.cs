using System;

namespace Inlook_Core.Models
{
    public class GetUserWithIdModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
