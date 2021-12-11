using Identity.Package.Service;
using Newtonsoft.Json;

namespace Identity.Package.Application;

public static class ApplicationExtensions
{
    private static IApiCall _apiCall;

    private static IDictionary<string, string> _headers;

    public static async Task<IEnumerable<User>> UsersAsync(this Application application)
        => await Task.Run(async () =>
        {
            RegisterDependency();
            ApiResponse response = await _apiCall.PostAsync("Application/GetUsers", application);
            UserResponse data = JsonConvert.DeserializeObject<UserResponse>(response.Result.ToString());
            return data.Users;
        });

    public static async Task UsersAsync(this Application application, int page, int count)
    {

    }

    private static void RegisterDependency()
    {
        _headers = new Dictionary<string, string>()
        {
            ["content-type"] = "application/json",
            ["connection"] = "keep-alive"
        };
        _apiCall = new ApiCall(_headers, new HttpClient(), "https://localhost:7130/api/");
    }
}
