using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SeniorActivitySupportSystem.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public Address Address { get; set; }

        public ICollection<SportEvent> SportEvents { get; set; }
        public ICollection<SportGroup> SportGroups { get; set; }
    }
}
