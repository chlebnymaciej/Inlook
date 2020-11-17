using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    public class UpdateGroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Users { get; set; }
    }
}
