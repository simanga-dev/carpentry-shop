
using CarpentryShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class SeedData
{
    public async Task Seed()
    {
        try
        {
            var roleStore = new RoleStore<IdentityRole>(DbContextOptions);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            var roles = roleMngr.Roles.ToList();

            if (RoleManager.Roles.ToList().Count == 0)
            {

            }

            if (UserManager.Users.ToList().Count == 0)
            {
                UserEntity entity = new UserEntity
                {
                    Email = "y",
                    Active = true,
                    Deleted = false,
                    EmailConfirmed = true,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                    Name = "y",
                    UserName = "x"
                };
                await UserManager.CreateAsync(entity, "fg@123");
                await UserManager.AddToRoleAsync(entity, RoleConst.Admin);
                //Send Invitation email to Admin in the Production.
            }

        }
        catch (Exception ex)
        {
            LogManager.LogError(JsonConvert.SerializeObject(new { class_name = this.GetType().Name, exception = ex }));
        }
    }




}
