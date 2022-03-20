namespace Identity.Service.Services;

public class SessionServices : ISessionRules, IDisposable
{
    private readonly IBaseRules<Session> _sessionCrud;

    public SessionServices(IBaseRules<Session> sessionCrud)
    {
        _sessionCrud = sessionCrud;
    }

    public async Task<bool> AnyAsync(Expression<Func<Session, bool>> any)
            => await _sessionCrud.AnyAsync(any);

    public async Task<Session> CreateSessionAsync(User user, Application application)
    => await Task.Run(async () =>
    {
        Session session = new()
        {
            CreateDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(30),
            Key = "I-Authentication",
            Value = Guid.NewGuid().ToString().CreateSHA256(),
            UserId = user.Id,
            ApplicationId = application.Id,
        };
        return await _sessionCrud.InsertAsync(session) ? session : null;
    });

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<Session>> GetApplicationSessionAsync(Guid appId)
        => await Task.Run(async () => await _sessionCrud.GetAsync(s => s.ApplicationId == appId));

    public async Task<IEnumerable<Session>> GetApplicationSessionAsync(Application application)
        => await Task.FromResult(await GetApplicationSessionAsync(application.Id));

    public async Task<Session> GetSessionAsync(HttpContext context)
            => await Task.Run(async () =>
            {
                string value = context.Request.Headers["I-Authentication"].ToString();
                return !string.IsNullOrEmpty(value) ? await _sessionCrud.GetOneAsync(s => s.Value == value) : null;
            });

    public async Task<Session> GetSessionAsync(string value)
        => await Task.FromResult(await _sessionCrud.GetOneAsync(s => s.Value == value));
}
