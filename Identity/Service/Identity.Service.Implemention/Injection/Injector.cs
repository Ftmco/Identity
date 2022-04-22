using Identity.DataBase.Entity;
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
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<User, IdentityContext>, BaseCud<User, IdentityContext>>();
        services.AddScoped<IBaseCud<Session, IdentityContext>, BaseCud<Session, IdentityContext>>();
        services.AddScoped<IBaseCud<Profile, IdentityContext>, BaseCud<Profile, IdentityContext>>();
        services.AddScoped<IBaseCud<ProfileImage, IdentityContext>, BaseCud<ProfileImage, IdentityContext>>();
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
        services.AddTransient<IFastAccountAction, FastAccountAction>();
        services.AddTransient<IProfileAction, ProfileAction>();
        services.AddTransient<IProfileGet, ProfileGet>();
        services.AddTransient<IProfileViewModel, ProfileViewModel>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        return Task.FromResult(services);
    }
}