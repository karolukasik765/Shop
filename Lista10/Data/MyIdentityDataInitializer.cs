using Microsoft.AspNetCore.Identity;
using System;

namespace Lista10.Data
{
    public class MyIdentityDataInitializer
    {

        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] rolesArray = { "Admin", "Customer"};

            SeedRoles(roleManager, rolesArray);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            SeedUser(userManager, "adminuser@localhost", "P@ssw0rd", "Admin");
            SeedUser(userManager, "customer@localhost", "Cu$t0mer", "Customer");
            SeedUser(userManager, "noname@localhost", "N0n@mee");

        }

        public static void SeedUser(UserManager<IdentityUser> userManager, string name, string password, string role = null)
        {
            if (userManager.FindByEmailAsync(name).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = name,
                    Email = name
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
                
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, string[] rolesArray)
        {
            foreach (string roleName in rolesArray) {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = roleName,
                    };
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }
        }
    }
}
