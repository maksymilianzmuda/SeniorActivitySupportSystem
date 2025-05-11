using SeniorActivitySupportSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorActivitySupportSystem.ViewModel
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string? Bio { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public IFormFile? Image { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<SportEvent> SportEvents { get; set; }
        public ICollection<SportGroup> SportGroups { get; set; }
    }
}
