namespace Inlook_API.Models
{
    public class Mail
    {
        public User From { get; set; }
        public User[] To { get; set; }
        public User[] CC { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
