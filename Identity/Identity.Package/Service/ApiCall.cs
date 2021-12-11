using Newtonsoft.Json;
using System.Text;

namespace Identity.Package.Service;

public class ApiCall : IApiCall, IDisposable
{
    private readonly IDictionary<string, string> _headers;

    private readonly HttpClient _httpClient;

    private readonly string _baseURL;

    public ApiCall(IDictionary<string, string> headers, HttpClient httpClient, string baseURL)
    {
        _headers = headers;
        _httpClient = httpClient;
        _baseURL = baseURL;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<ApiResponse?> GetAsync(string url)
        => await Task.Run(async () =>
        {
            url = _baseURL + url;

            using HttpRequestMessage httpRequest = new();

            httpRequest.RequestUri = new Uri(url);
            httpRequest.Method = HttpMethod.Get;

            SetHeader(_headers, _httpClient);
            using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(content);
        });

    public async Task<ApiResponse?> PostAsync(string url, object data)
        => await Task.Run(async () =>
        {
            url = _baseURL + url;

            using HttpRequestMessage httpRequest = new();
            httpRequest.RequestUri = new Uri(url);
            string? json = JsonConvert.SerializeObject(data);
            StringContent? dataJson = new(json, Encoding.UTF8, "application/json");
            httpRequest.Content = dataJson;
            httpRequest.Method = HttpMethod.Post;
            SetHeader(_headers, _httpClient);

            using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(content);
        });

    public void SetHeader(IDictionary<string, string> headers, HttpClient client)
    {
        foreach (KeyValuePair<string, string> item in headers)
            _ = client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
    }
}

public record ApiResponse(bool Status, short Code, string? Title, string? Message, object Result);