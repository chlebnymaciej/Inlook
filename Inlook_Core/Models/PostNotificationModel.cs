
namespace Inlook_Core.Models
{
    /// <summary>
    /// Model for creating notification.
    /// </summary>
    public class PostNotificationModel
    {
        /// <summary>
        /// Notification content.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// List of notifications recepients.
        /// </summary>
        public string[] RecipientsList { get; set; }
        /// <summary>
        /// Indicates if notification contains attachment.
        /// </summary>
        public bool WithAttachments { get; set; }
    }
}
