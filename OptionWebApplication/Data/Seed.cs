using Microsoft.AspNetCore.Identity;
using OptionWebApplication.Data;
using OptionWebApplication.Models;

namespace OptionWebApplication.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
                if (!context.Assemblies.Any())
                {
                    context.Assemblies.AddRange(new List<Assembly>()
                    {
                        new Assembly()
                        {
                            SerialNumber = "111111",
                            TypeDevice = Enum.TypeDevice.Монитор,
                            ChangeComponents = "Материнская плата",
                            OtherWork = "Гравировка",
                            Step1 = "Full",
                            People1 = Enum.People.Дмитрий1
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Guarentes.Any())
                {
                    context.Guarentes.AddRange(new List<Guarentee>()
                    {
                        new Guarentee()
                        {
                            SerialNumber = "222222",
                            TypeDevice = Enum.TypeDevice.Ноутбук,
                            DateIn = new DateTime(2022,08,26),
                            DateOut = new DateTime(2022,08,30),
                            Details = "Не включаеться",
                            FaultDetection = "Материнская плата не работает",
                            Conclusion = "Замена материнской платы по гарантии",
                            DiagnosticPeople = Enum.People.Дмитрий1,
                            ComplectedWork = "Замена метринской платы, замена термопасты, чистка от пыли",
                            RepairPeople = Enum.People.Дмитрий1

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
                string adminUserEmail = "dshohov@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "dshohov",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "10091778");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
