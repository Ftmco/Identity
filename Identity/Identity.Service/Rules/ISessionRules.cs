using Identity.Entity.User;

namespace Identity.Service.Rules;

public interface ISessionRules
{
    Task<Session> GetSessionAsync(HttpContext context);

    Task<Session> GetSessionAsync(string value);

    Task<Session> CreateSessionAsync(User user,Application application);

    Task<IEnumerable<Session>> GetApplicationSessionAsync(Guid appId);

    Task<IEnumerable<Session>> GetApplicationSessionAsync(Application application);
}
