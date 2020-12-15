
namespace Inlook_Core.Models
{
    public class PostNotificationModel
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string[] RecipientsList { get; set; }
        public bool WithAttachments { get; set; }

    }
}
