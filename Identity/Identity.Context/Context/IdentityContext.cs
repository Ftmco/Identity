using Identity.Entity.Application;
using Identity.Entity.Page;
using Identity.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace Identity.Context;

public class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    { }

    public DbSet<User> User { get; set; }

    public DbSet<Application> Application { get; set; }

    public DbSet<ApplicationUsers> ApplicationUsers { get; set; }

    public DbSet<Page> Page { get; set; }

    public DbSet<RolePages> RolePages { get; set; }
}
