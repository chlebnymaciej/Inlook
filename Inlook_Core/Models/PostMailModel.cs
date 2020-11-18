using System;

namespace Inlook_Core.Models
{
    public class PostMailModel
    {
        public string[] To { get; set; }
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
