using Identity.DataBase.Entity;
using Identity.Service.Tools.Crypto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Identity.Service.Implemention;

public class SessionAction : ISessionAction
{
    readonly IConfiguration _configuration;

    readonly IBaseCud<Session, IdentityContext> _sessionCud;

    public SessionAction(IConfiguration configuration, IBaseCud<Session, IdentityContext> sessionCud)
    {
        _sessionCud = sessionCud;
        _configuration = configuration;
    }

    public async Task<Session?> CreateSessionAsync(User user)
    {
        string key = _configuration["Identity:Key"];
        string userJson = JsonConvert.SerializeObject(user with { Password = "", ActiveCode = "" });
        string encUser = CryptoEngine.Encrypt(userJson, key);
        Session session = new()
        {
            Key = "Auth-Token",
            CreateDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddDays(15),
            Os = "",
            UserId = user.Id,
            Value = encUser
        };
        return await _sessionCud.InsertAsync(session) ? session : null;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
