using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Inlook_Core.Models.Attachments
{
    public class GetFileModel
    {
        public string ClientFileName { get; set; }

        public Stream FileStream { get; set; }
    }
}
