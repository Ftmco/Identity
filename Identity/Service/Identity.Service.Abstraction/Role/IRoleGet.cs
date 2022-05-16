namespace Identity.Service.Abstraction;

public interface IRoleGet : IAsyncDisposable
{
    Task<IDictionary<RolesUsers, Role>> GetUserApplicationRolesAsync(Guid appUserId);
}
