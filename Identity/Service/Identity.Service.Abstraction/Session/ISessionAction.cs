using System.Linq.Expressions;

namespace Identity.Service.Abstraction;

public interface ISessionAction : IAsyncDisposable
{
    Task<DataBase.Entity.Session?> CreateSessionAsync(User user, Guid appId);

    Task DeleteSessionAsync(string session);

}
