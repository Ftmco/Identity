using Identity.ViewModels.Api;
using Tools.Crypto;

namespace Identity.Tools.Api;

public static class ApiHelper
{
    public static async Task<ApiRequest> SendResponseAsync(this ApiModel api, HttpContext httpContext)
        => await Task.Run(() =>
        {
            string? key = httpContext.KeyMaker();
            string json = JsonConvert.SerializeObject(api);
            string? encodeData = json.Encrypt(key);
            return new ApiRequest(encodeData);
        });

    public static async Task<TRequest?> ReadRequestDataAsync<TRequest>(this ApiRequest request, HttpContext httpContext)
        => await Task.Run(() =>
        {
            string? key = httpContext.KeyMaker();
            string? decodeData = request.Data.Decrypt(key);
            TRequest? requestModel = JsonConvert.DeserializeObject<TRequest>(decodeData);
            return requestModel;
        });
}