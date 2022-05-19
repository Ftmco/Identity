namespace Identity.Service.Abstraction;

public interface IRoleAction : IAsyncDisposable
{
    /// <summary>
    /// Check User role and insert it when is not exist
    /// </summary>
    /// <param name="appUserId">Application User Id <see cref="ApplicationsUsers"/></param>
    /// <param name="roleId">Role Id <see cref="Role"/></param>
    /// <returns><see cref="RolesUsers"/> Instance</returns>
    Task<RolesUsers> CheckUserRoleAsync(Guid appUserId, Guid roleId);
}
