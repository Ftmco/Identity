namespace Identity.Service.Abstraction;

public interface ISessionGet : IAsyncDisposable
{
    Task<DataBase.Entity.Session> GetSessionAsync(string value);
}
