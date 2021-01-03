using System;

namespace Inlook_Core.Models
{
    public class GetUserWithIdModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
