using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Models.Attachments
{
    /// <summary>Mail attachment model.</summary>
    public class GetAttachmentModel
    {
        /// <summary>Id of attachment.</summary>
        public Guid Id { get; set; }
        /// <summary>Name of file on client side.</summary>
        public string ClientFileName { get; set; }
        /// <summary>Name of file on azure server.</summary>
        public string AzureFileName { get; set; }
    }
}
