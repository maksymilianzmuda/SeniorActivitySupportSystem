using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.ViewModel
{
    public class DashboardViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? Bio { get; set; }
        public List<SportGroup> SportGroups { get; set; }
        public List<SportEvent> SportEvents { get; set; }
    }
}
