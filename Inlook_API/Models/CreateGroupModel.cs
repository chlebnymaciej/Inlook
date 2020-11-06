namespace Inlook_API.Models
{
    public class CreateGroupModel
    {
        public string Name { get; set; }
        public User Owner { get; set; }
        public User[] Users { get; set; }
    }
}
