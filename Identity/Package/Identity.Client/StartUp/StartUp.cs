using Identity.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Client.StartUp;

public static class StartUp
{
    public static IServiceCollection AddFteamIdentityService(this IServiceCollection service)
    {
        service.AddTransient<IAccountAction, AccountAction>();
        service.AddTransient<ICache, Cache.Cache>();
        service.AddTransient<IGrpcRule, GrpcService>();
        service.AddTransient<IUserGet, UserGet>();
        service.AddTransient<IOtpAccountAction, OtpAccountAction>();
        return service;
    }
}
