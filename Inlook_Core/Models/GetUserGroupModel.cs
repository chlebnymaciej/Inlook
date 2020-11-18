using System;

namespace Inlook_Core.Models
{
    public class GetUserGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GetUserWithIdModel[] Users { get; set; }
    }
}
