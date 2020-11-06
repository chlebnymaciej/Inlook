using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Entities
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
