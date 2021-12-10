using Identity.Entity.User;

namespace Identity.Service.Rules;

public interface ISessionRules
{
    Task<Session> GetSessionAsync(HttpContext context);

    Task<Session> CreateSessionAsync(User user);
}
