using CeilApp.Data;
using CeilApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CeilApp.Services
{
    public class Globals
    {
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Student = "Student";

        public static AppSetting appSettings { get;  set; }

        public static async Task EnsureParamAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { Admin, Teacher, Student };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminUser = await userManager.FindByEmailAsync("djellal@univ-setif.dz");

            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "djellal@univ-setif.dz",
                    Email = "djellal@univ-setif.dz",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "DhB@571982");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Admin);
                }
            }

            var dbcontext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            appSettings = dbcontext.AppSettings.FirstOrDefault();
            if (appSettings == null)
            {
                appSettings = new AppSetting
                {
                    Id = 1,
                    OrganizationName= "CEIL de l’UFAS1",
                    Email= "ceil@univ-setif.dz",
                    Tel= "(+213) 036.62.09.96",
                    Logo="",
                    Address= "Université Sétif -1-\r\nCampus El Bez, \r\nEx-Faculté de Droit (Actuellement Département d'Agronomie)",
                    WebSite = "https://ceil@univ-setif.dz",
                    FB = "https://www.facebook.com/CEIL.SETIF1UNIVERSITY",
                    LinkredIn= "https://www.linkedin.com/school/universite-ferhat-abbas-setif",
                    IsRegistrationOpened=false
                };
                dbcontext.Add(appSettings);
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
