using Identity.Entity.User;
using Identity.Package.Models;
using Identity.Package.Rules;
using Identity.Package.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Crypto;

namespace Identity.Package.Extensions;

public static class ApplicationExtensions
{
    readonly static IApiCall _apiCall = new ApiCall(new HttpClient { BaseAddress = new Uri("") });

    public static async Task<GetAllUsers> GetAllUsersAsync(this Application application)
    {
        string reqUrl = "/api/User/GetUsers";
        string key = reqUrl.KeyMaker();
        string data = JsonConvert.SerializeObject(application).Encrypt(key);
        string request = await _apiCall.PostAsync(reqUrl, new { data });
        ApiModel response = JsonConvert.DeserializeObject<ApiModel>(request);
        if (response.Status)
        {
            IEnumerable<User> users = (IEnumerable<User>)response.Result;
            return new GetAllUsers(ActionStatus.Success, users);
        }
        return new GetAllUsers(ActionStatus.Exception, null);
    }

    public static async Task<GetUser> GetUserByTokenAsync(this Application application, string token)
    {
        string reqUrl = "/api/User/GetByToken";
        var model = new
        {
            application,
            token
        };
        string key = reqUrl.KeyMaker();
        string data = JsonConvert.SerializeObject(model).Encrypt(key);
        string request = await _apiCall.PostAsync(reqUrl, new { data });
        ApiModel response = JsonConvert.DeserializeObject<ApiModel>(request.Decrypt(key));
        if (response.Status)
        {
            User user = (User)response.Result;
            return new GetUser(ActionStatus.Success, user);
        }
        return new GetUser(ActionStatus.Exception, null);
    }

    public static async Task<GetUser> GetUserByUserNameAsync(this Application application, string userName)
    {
        string reqUrl = "/api/User/GetByUserName";
        var model = new
        {
            application,
            userName
        };
        string key = reqUrl.KeyMaker();
        string data = JsonConvert.SerializeObject(model).Encrypt(key);
        string request = await _apiCall.PostAsync(reqUrl, new { data });
        ApiModel response = JsonConvert.DeserializeObject<ApiModel>(request.Decrypt(key));
        if (response.Status)
        {
            User user = (User)response.Result;
            return new GetUser(ActionStatus.Success, user);
        }
        return new GetUser(ActionStatus.Exception, null);
    }
}