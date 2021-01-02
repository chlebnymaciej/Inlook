using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inlook_Core.Models
{
    /// <summary>
    /// Model for editing group info.
    /// </summary>
    public class UpdateGroupModel
    {
        /// <summary>
        /// Id of group.
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// New group name.
        /// </summary>
        [MinLength(3, ErrorMessage = "Group name must contain at least 3 characters")]
        [MaxLength(40, ErrorMessage = "Group name must be equal to or less than 40 characters")]
        public string Name { get; set; }

        /// <summary>
        /// New group members.
        /// </summary>
        [MinLength(1)]
        public string[] Users { get; set; }
    }
}
