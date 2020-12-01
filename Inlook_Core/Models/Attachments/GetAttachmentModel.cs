using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models.Attachments
{
    public class GetAttachmentModel
    {
        public Guid Id { get; set; }
        public string ClientFileName { get; set; }
        public string AzureFileName { get; set; }
    }
}
