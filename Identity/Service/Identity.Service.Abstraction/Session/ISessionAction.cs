namespace Identity.Service.Abstraction;

public interface ISessionAction : IAsyncDisposable
{
    Task<DataBase.Entity.Session?> CreateSessionAsync(User user);
}
