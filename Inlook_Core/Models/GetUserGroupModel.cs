namespace Inlook_Core.Models
{
    public class GetUserGroupModel
    {
        public string Name { get; set; }
        public GetUserWithIdModel[] Users { get; set; }
    }
}
