using Identity.DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataBase.Context;

public class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }

    public IdentityContext()
    {

    }

    public static string ConnectionString { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //"host=localhost;database=identity_db;user id=fteam;password=1G14ijWA;"

        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(ConnectionString);
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Session> Session { get; set; }

    public virtual DbSet<Profile> Profile { get; set; }

    public virtual DbSet<ProfileImage> ProfileImage { get; set; }

    public virtual DbSet<ApplicationsUsers> ApplicationsUsers { get; set; }

    public virtual DbSet<Application> Application { get; set; }

    public virtual DbSet<RolesUsers> RolesUsers { get; set; }
}
