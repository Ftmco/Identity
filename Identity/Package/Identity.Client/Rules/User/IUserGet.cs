using Identity.Client.Models;

namespace Identity.Client.Rules;

public interface IUserGet : IAsyncDisposable
{
    Task<User?> GetUserBySessionAsync(string session);

    Task<User?> GetUserByUserNameAsync(string username);

    Task<User?> GetUserByIdAsync(Guid userId);

    Task GetUsersStreamAsync(IEnumerable<Guid> userIds, Action<User> user);

    Task<IEnumerable<User>> GetUsersStreamAsync(IEnumerable<Guid> userIds);
}
