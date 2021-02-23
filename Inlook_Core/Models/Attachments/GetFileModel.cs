using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Inlook_Core.Models.Attachments
{
    /// <summary>
    /// Model for returning file with user info.
    /// </summary>
    public class GetFileModel
    {
        /// <summary>
        /// Name of file on user side.
        /// </summary>
        public string ClientFileName { get; set; }

        public Stream FileStream { get; set; }

        public string ContentType { get; set; }
    }
}
