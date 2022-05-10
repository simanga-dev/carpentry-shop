using CarpentryShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class SampleData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<ApplicationDbContext>();

        string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == role))
            {
                var oRole = new IdentityRole
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                var result = roleStore.CreateAsync(oRole).Result;
            }
        }

        var user = new User
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@copalcor.co.za",
            UserName = "admin@copalcor.co.za",
            NormalizedEmail = "ADMIN@COPALCOR.CO.ZA",
            NormalizedUserName = "ADMIN@COPALCOR.CO.ZA",
            PhoneNumber = "+111111111111",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            LockoutEnabled = false
            // SecurityStamp = Guid.NewGuid().ToString("D")
            //
        };


        if (!context.Users.Any(u => u.UserName == user.UserName))
        {

            UserManager<User> _userManager = serviceProvider.GetService<UserManager<User>>();

            var results = await _userManager.CreateAsync(user, "P@ssword1");

            // var password = new PasswordHasher<User>();
            // var hashed = password.HashPassword(user, "P@ssword1");
            // user.PasswordHash = hashed;

            // var userStore = new UserStore<User>(context);
            // var result = userStore.CreateAsync(user);

        }

        await AssignRoles(serviceProvider, user.Email, roles);
        await context.SaveChangesAsync();
    }

    public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
    {
        UserManager<User> _userManager = services.GetService<UserManager<User>>();

        System.Console.WriteLine(_userManager.ToString());
        User user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result;
    }
}
