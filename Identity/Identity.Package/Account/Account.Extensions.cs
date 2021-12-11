using Identity.Package.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Account;

public static class AccountExtensions
{
    private static IApiCall _apiCall;

    private static IDictionary<string, string> _headers;


    public static async Task<Session> LoginAsync(this Application.Application application, Login login)
        => await Task.Run(async () =>
        {
            RegisterDependency();
            var model = new
            {
                login.UserName,
                login.Password,
                application
            };
            ApiResponse response = await _apiCall.PostAsync("Account/Login", model);
            return response.Status ? JsonConvert.DeserializeObject<Session>(response.Result.ToString()) : null;
        });

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