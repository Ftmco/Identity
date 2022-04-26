using Identity.DataBase.Entity;

namespace Identity.Service.Implemention;

public class SessionGet : ISessionGet
{
    readonly IBaseQuery<Session, IdentityContext> _sessionQuery;

    public SessionGet(IBaseQuery<Session, IdentityContext> sessionQuery)
    {
        _sessionQuery = sessionQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Session?> GetSessionAsync(string value)
    {
        return await _sessionQuery.GetAsync(s => s.Value == value);
    }
}
