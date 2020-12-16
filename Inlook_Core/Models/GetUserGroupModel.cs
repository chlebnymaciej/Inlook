using System;

namespace Inlook_Core.Models
{
    public class GetUserGroupModel
    {
        /// <summary>
        /// Group Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of groups
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Group members
        /// </summary>
        public GetUserWithIdModel[] Users { get; set; }
    }
}
