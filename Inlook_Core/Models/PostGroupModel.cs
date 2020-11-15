using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models
{
    public class PostGroupModel
    {
        public string  Name { get; set; }
        public string[] Users { get; set; }
    }

    public class PostGroupEntityModel
    {
        public Guid Owner { get; set; }
        public string Name { get; set; }
        public Guid[] Users { get; set; }
    }
}
