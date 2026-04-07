using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarpentryShop.Areas.Identity.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarpentryShopIdentityDbContext>
{
    public CarpentryShopIdentityDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
            ?? throw new Exception("DB_CONNECTION environment variable is not set");

        var optionsBuilder = new DbContextOptionsBuilder<CarpentryShopIdentityDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CarpentryShopIdentityDbContext(optionsBuilder.Options);
    }
}
