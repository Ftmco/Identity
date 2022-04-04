using Microsoft.EntityFrameworkCore;

namespace Identity.DataBase.Context;

public class IdentityContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("host=localhost;database=identity_db;user id=fteam;password=1G14ijWA;");
        }
    }
}
