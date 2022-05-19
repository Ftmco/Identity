using Identity.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Client.StartUp;

public static class StartUp
{
    public static IServiceCollection AddFteamIdentityAuthService(this IServiceCollection service)
    {
        return service;
    }
}
