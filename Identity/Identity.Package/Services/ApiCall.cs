using Identity.Package.Models;
using Identity.Package.Rules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Services;

public class ApiCall : IApiCall
{
    readonly HttpClient _httpClient;

    public ApiCall(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<string> GetAsync(string url)
    {
        HttpResponseMessage request = await _httpClient.GetAsync(url);
        return request.IsSuccessStatusCode ? await request.Content.ReadAsStringAsync() : "";
    }

    public async Task<string> PostAsync<TValue>(string url, TValue value)
    {
        var request = await _httpClient.PostAsJsonAsync(url, value);
        return request.IsSuccessStatusCode ? await request.Content.ReadAsStringAsync() : "";
    }
}
