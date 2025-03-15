using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<SportGroup> SportGroups { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
