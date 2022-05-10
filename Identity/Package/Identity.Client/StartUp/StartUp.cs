using Microsoft.Extensions.DependencyInjection;
using Identity.Client.Services;
using Identity.Client.Rules;
using Identity.Client.Cache;

namespace Identity.Client.StartUp;

public static class StartUp
{
    public static IServiceCollection AddFteamIdentityService(this IServiceCollection service)
    {
        service.AddTransient<IAccountAction, AccountAction>();
        service.AddTransient<ICache, Cache.Cache>();
        service.AddTransient<IGrpcRule, GrpcService>();
        service.AddTransient<IUserGet, UserGet>();
        return service;
    }
}
