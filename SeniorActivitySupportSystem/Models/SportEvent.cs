using SeniorActivitySupportSystem.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorActivitySupportSystem.Models
{
    public class SportEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public EventCategory EventCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
