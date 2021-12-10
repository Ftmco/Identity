using Identity.Entity.User;
using Identity.Service.Rules;
using Identity.Services.Base;

namespace Identity.Service.Services;

public class SessionServices : ISessionRules, IDisposable
{
    private readonly IBaseRules<Session> _sessionCrud;

    public SessionServices(IBaseRules<Session> sessionCrud)
    {
        _sessionCrud = sessionCrud;
    }

    public async Task<Session> CreateSessionAsync(User user)
    => await Task.Run(async () =>
    {
        Session session = new()
        {
            CreateDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(30),
            Key = "I-Authentication",
            Value = Guid.NewGuid().ToString().CreateSHA256(),
            UserId = user.Id
        };
        return await _sessionCrud.InsertAsync(session) ? session : null;
    });

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<Session> GetSessionAsync(HttpContext context)
            => await Task.Run(async () =>
            {
                string value = context.Request.Headers["I-Authentication"].ToString();
                return !string.IsNullOrEmpty(value) ? await _sessionCrud.GetOneAsync(s => s.Value == value) : null;
            });
}
