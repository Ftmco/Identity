using Identity.Client.Models;

namespace Identity.Client.Rules;

public interface IAccountRules : IAsyncDisposable
{
    Task<User?> GetUserCacheAsync(string session);
}
