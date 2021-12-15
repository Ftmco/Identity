namespace Identity.Web.Server;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddServices();
        services.AddBaseServices();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountRules, AccountServices>();
        services.AddScoped<IApplicationRules, ApplicationServices>();
        services.AddScoped<ISessionRules, SessionServices>();
        services.AddScoped<IProfileRules, ProfileService>();
        return services;
    }

    private static IServiceCollection AddBaseServices(this IServiceCollection services)
    {
        services.AddTransient<IBaseRules<User>, BaseServices<User>>();
        services.AddTransient<IBaseRules<UserRoles>, BaseServices<UserRoles>>();
        services.AddTransient<IBaseRules<Role>, BaseServices<Role>>();
        services.AddTransient<IBaseRules<Application>, BaseServices<Application>>();
        services.AddTransient<IBaseRules<ApplicationUsers>, BaseServices<ApplicationUsers>>();
        services.AddTransient<IBaseRules<Page>, BaseServices<Page>>();
        services.AddTransient<IBaseRules<RolePages>, BaseServices<RolePages>>();
        services.AddTransient<IBaseRules<Session>, BaseServices<Session>>();
        services.AddTransient<IBaseRules<Profile>, BaseServices<Profile>>();
        return services;
    }
}
