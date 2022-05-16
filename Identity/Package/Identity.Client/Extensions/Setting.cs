using Newtonsoft.Json;

namespace Identity.Client.Extensions;

public static class AppSettings
{
    public static Application GetApplication(this IConfiguration configuration)
    {
        string? key = configuration["Application:Key"];
        string? apiKey = configuration["Application:ApiKey"];
        return new(apiKey, key);
    }

    public static string GetApplicationJson(this IConfiguration configuration)
    {
        var application = configuration.GetApplication();
        return JsonConvert.SerializeObject(application);
    }
}