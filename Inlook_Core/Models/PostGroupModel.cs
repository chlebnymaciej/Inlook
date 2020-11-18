using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inlook_Core.Models
{
    public class PostGroupModel
    {
        [Required]
        [MinLength(3,ErrorMessage = "Group name has to be at least 3 characters long")]
        public string  Name { get; set; }
        [MinLength(1)]
        public string[] Users { get; set; }
    }

}
