using Microsoft.Extensions.DependencyInjection;
using Identity.Client.Services;
using Identity.Client.Rules;
using Identity.Client.Cache;

namespace Identity.Client.StartUp;

public static class StartUp
{
    public static IServiceCollection AddFteamIdentityService(this IServiceCollection service)
    {
        service.AddTransient<IAccountRules, AccountService>();
        service.AddTransient<ICache, Cache.Cache>();
        service.AddTransient<IGrpcRule, GrpcService>();
        return service;
    }
}
