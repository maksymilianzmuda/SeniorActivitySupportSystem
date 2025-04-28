using Microsoft.AspNetCore.Identity;
using SeniorActivitySupportSystem.Data.Enum;
using SeniorActivitySupportSystem.Models;
using System.Diagnostics;

namespace SeniorActivitySupportSystem.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.SportGroups.Any())
                {
                    context.SportGroups.AddRange(new List<SportGroup>()
                    {
                        new SportGroup()
                        {
                            Name = "SportGroup1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            SportGroupCategory = SportGroupCategory.MenOnly,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                         },
                        new SportGroup()
                        {
                            Name = "SportGroup2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            SportGroupCategory = SportGroupCategory.MenOnly,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                         },
                        new SportGroup()
                        {
                            Name = "SportGroup3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            SportGroupCategory = SportGroupCategory.MenOnly,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                         },
                        new SportGroup()
                        {
                            Name = "SportGroup4",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            SportGroupCategory = SportGroupCategory.MenOnly,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                         }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.SportEvents.Any())
                {
                    context.SportEvents.AddRange(new List<SportEvent>()
                    {
                        new SportEvent()
                        {
                            Name = "Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            EventCategory = EventCategory.Cycling,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                        },
                       new SportEvent()
                        {
                            Name = "Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            EventCategory = EventCategory.Cycling,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                PostalCode = "41-717"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FirstName = "AdminFN",
                        LastName = "AdminLN",
                        Gender = "M",
                        Bio = "Bio",
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            PostalCode = "NC-412"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FirstName = "UserFN",
                        LastName = "UserLN",
                        Gender = "M",
                        UserName = "User",
                        Bio = "Bio",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            PostalCode = "NC-412"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
