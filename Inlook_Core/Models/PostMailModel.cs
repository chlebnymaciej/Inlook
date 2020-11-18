using System;
using System.ComponentModel.DataAnnotations;

namespace Inlook_Core.Models
{
    public class PostMailModel
    {
        [MinLength(1)]
        public string[] To { get; set; }
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        [MinLength(1)]
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
