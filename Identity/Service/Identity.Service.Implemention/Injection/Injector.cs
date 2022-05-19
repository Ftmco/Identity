using Identity.Service.Implemention;
using Identity.Service.Implemention.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Servant.Service.Implemention.Injector;

public static class Injector
{
    public static async Task<IServiceCollection> AddIdentityServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            string cnn = configuration.GetConnectionString("Identity_Db");
            IdentityContext.ConnectionString = cnn;
            options.UseNpgsql(cnn);
        });
        await services.AddBaseCudAsync();
        await services.AddBaseQueryAsync();
        await services.AddServicesAsync();
        await services.AddToolsAsync();
        return services;
    }

    public static Task<IServiceCollection> AddBaseQueryAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<User, IdentityContext>, BaseQuery<User, IdentityContext>>();
        services.AddScoped<IBaseQuery<Session, IdentityContext>, BaseQuery<Session, IdentityContext>>();
        services.AddScoped<IBaseQuery<Profile, IdentityContext>, BaseQuery<Profile, IdentityContext>>();
        services.AddScoped<IBaseQuery<ProfileImage, IdentityContext>, BaseQuery<ProfileImage, IdentityContext>>();
        services.AddScoped<IBaseQuery<Application, IdentityContext>, BaseQuery<Application, IdentityContext>>();
        services.AddScoped<IBaseQuery<ApplicationsUsers, IdentityContext>, BaseQuery<ApplicationsUsers, IdentityContext>>();
        services.AddScoped<IBaseQuery<Role, IdentityContext>, BaseQuery<Role, IdentityContext>>();
        services.AddScoped<IBaseQuery<RolesUsers, IdentityContext>, BaseQuery<RolesUsers, IdentityContext>>();
        services.AddScoped<IBaseQuery<Setting, IdentityContext>, BaseQuery<Setting, IdentityContext>>();
        services.AddScoped<IBaseQuery<Page, IdentityContext>, BaseQuery<Page, IdentityContext>>();
        services.AddScoped<IBaseQuery<PagesRoles, IdentityContext>, BaseQuery<PagesRoles, IdentityContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<User, IdentityContext>, BaseCud<User, IdentityContext>>();
        services.AddScoped<IBaseCud<Session, IdentityContext>, BaseCud<Session, IdentityContext>>();
        services.AddScoped<IBaseCud<Profile, IdentityContext>, BaseCud<Profile, IdentityContext>>();
        services.AddScoped<IBaseCud<ProfileImage, IdentityContext>, BaseCud<ProfileImage, IdentityContext>>();
        services.AddScoped<IBaseCud<Application, IdentityContext>, BaseCud<Application, IdentityContext>>();
        services.AddScoped<IBaseCud<ApplicationsUsers, IdentityContext>, BaseCud<ApplicationsUsers, IdentityContext>>();
        services.AddScoped<IBaseCud<Role, IdentityContext>, BaseCud<Role, IdentityContext>>();
        services.AddScoped<IBaseCud<RolesUsers, IdentityContext>, BaseCud<RolesUsers, IdentityContext>>();
        services.AddScoped<IBaseCud<Setting, IdentityContext>, BaseCud<Setting, IdentityContext>>();
        services.AddScoped<IBaseCud<Page, IdentityContext>, BaseCud<Page, IdentityContext>>();
        services.AddScoped<IBaseCud<PagesRoles, IdentityContext>, BaseCud<PagesRoles, IdentityContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        services.AddTransient<IAccountAction, AccountAction>();
        services.AddTransient<IUserAction, UserAction>();
        services.AddTransient<IUserGet, UserGet>();
        services.AddTransient<IUserViewModel, UserViewModel>();
        services.AddTransient<ISessionAction, SessionAction>();
        services.AddTransient<ISessionGet, SessionGet>();
        services.AddTransient<IOtpAccountAction, OtpAccountAction>();
        services.AddTransient<IProfileAction, ProfileAction>();
        services.AddTransient<IProfileGet, ProfileGet>();
        services.AddTransient<IProfileViewModel, ProfileViewModel>();
        services.AddTransient<IApplicationGet, ApplicationGet>();
        services.AddTransient<IApplicationSettingGet, ApplicationSettingGet>();
        services.AddTransient<IApplicationAction, ApplicationAction>();
        services.AddTransient<IRoleAction, RoleAction>();
        services.AddTransient<IRoleGet, RoleGet>();
        services.AddTransient<IPageGet, PageGet>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        return Task.FromResult(services);
    }
}