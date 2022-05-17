using Identity.Service.Tools.Crypto;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

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

    public async Task<Session?> CreateSessionAsync(User user, Guid appId)
    {
        string key = _configuration["Identity:Key"];
        string sessionData = Guid.NewGuid().ToString();
        string encSession = CryptoEngine.Encrypt(sessionData, key);
        Session session = new()
        {
            Key = "Auth-Token",
            CreateDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddDays(15),
            Os = "",
            UserId = user.Id,
            Value = encSession,
            ApplicationId = appId
        };
        return await _sessionCud.InsertAsync(session) ? session : null;
    }

    public async Task DeleteSessionAsync(string session)
        => await _sessionCud.DeleteAsync(s => s.Value == session);

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
