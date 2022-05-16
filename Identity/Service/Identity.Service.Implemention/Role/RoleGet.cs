namespace Identity.Service.Implemention;

public class RoleGet : IRoleGet
{
    readonly IBaseQuery<RolesUsers, IdentityContext> _roleUsersQuery;

    readonly IBaseQuery<Role, IdentityContext> _roleQuery;

    public RoleGet(IBaseQuery<RolesUsers, IdentityContext> roleUsersQuery, IBaseQuery<Role, IdentityContext> roleQuery)
    {
        _roleUsersQuery = roleUsersQuery;
        _roleQuery = roleQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<IDictionary<RolesUsers, Role>> GetUserApplicationRolesAsync(Guid appUserId)
    {
        Dictionary<RolesUsers, Role> userRoles = new();

        IEnumerable<RolesUsers>? roles = await _roleUsersQuery.GetAllAsync(ru => ru.ApplicationsUsersId == appUserId);
        if (roles != null)
            foreach (var roleUser in roles)
            {
                var role = await _roleQuery.GetAsync(roleUser.RoleId);
                if (role != null)
                    userRoles.Add(roleUser, role);
            }

        return userRoles;
    }
}
