using Identity.Client.Models;
using Identity.Client.Rules;
using Identity.Service.Tools.Crypto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Identity.Client.Services;

public class AccountService : IAccountRules
{
    readonly IConfiguration _configuration;

    public AccountService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public Task<User?> GetUserCacheAsync(string session)
    {
        string? key = _configuration["Identity:Key"];
        string? decodeJson = CryptoEngine.Decrypt(session, key);
        User? user = JsonConvert.DeserializeObject<User>(decodeJson);
        return Task.FromResult(user);
    }
}
