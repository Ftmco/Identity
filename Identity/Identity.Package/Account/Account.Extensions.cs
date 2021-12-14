using Identity.Package.Application;
using Identity.Package.Service;
using Newtonsoft.Json;

namespace Identity.Package.Account;

public static class AccountExtensions
{
    private static IApiCall _apiCall;

    private static IDictionary<string, string> _headers;


    public static async Task<LoginResponse> LoginAsync(this Application.Application application, Login login)
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
            Session session = response.Status ?
                    JsonConvert.DeserializeObject<Session>(response.Result.ToString())
                        : null;

            return new LoginResponse(session)
            {
                Code = response.Code,
                Status = response.Status,
                Title = response.Title,
                Message = response.Message,
            };
        });

    public static async Task<SignUpResponse> SingUpAsync(this Application.Application application, SignUp signUp)
        => await Task.Run(async () =>
        {
            RegisterDependency();
            var model = new
            {
                signUp.UserName,
                signUp.FullName,
                signUp.MobileNo,
                signUp.Email,
                signUp.Password,
                Application = application
            };
            ApiResponse response = await _apiCall.PostAsync("Account/SignUp", model);
            Console.WriteLine(response);
            User user = default;
            return new SignUpResponse(user)
            {
                Code = response.Code,
                Title = response.Title,
                Message = response.Message,
                Status = response.Status,
            };
        });

    private static void RegisterDependency()
    {
        _headers = new Dictionary<string, string>()
        {
            ["content-type"] = "application/json",
            ["connection"] = "keep-alive"
        };
        _apiCall = new ApiCall(_headers, new HttpClient(), "https://localhost:5001/api/");
    }
}