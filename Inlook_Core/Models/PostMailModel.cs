using System;
using System.ComponentModel.DataAnnotations;

namespace Inlook_Core.Models
{
    public class PostMailModel
    {
        /// <summary>
        /// Mail recepients.
        /// </summary>
        [MinLength(1)]
        public string[] To { get; set; }
        /// <summary>
        /// Mail CC recepients.
        /// </summary>
        public string[] CC { get; set; }
        /// <summary>
        /// Mail BCC recepients.
        /// </summary>
        public string[] BCC { get; set; }

        /// <summary>
        /// Main subject.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Mail content text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Mail attachments.
        /// </summary>
        public string[] Attachments { get; set; }
    }
}
