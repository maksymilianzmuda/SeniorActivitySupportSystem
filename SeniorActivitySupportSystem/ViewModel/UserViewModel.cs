namespace SeniorActivitySupportSystem.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username {  get; set; }
        public IFormFile? Image { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}
