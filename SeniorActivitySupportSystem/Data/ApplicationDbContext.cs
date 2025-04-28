using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<SportGroup> SportGroups { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
