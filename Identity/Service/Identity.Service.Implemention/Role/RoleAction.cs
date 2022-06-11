namespace Identity.Service.Implemention;

public class RoleAction : IRoleAction
{
    readonly IBaseCud<RolesUsers, IdentityContext> _rolesUsersCud;

    readonly IBaseQuery<RolesUsers, IdentityContext> _rolesUsersQuery;

    public RoleAction(IBaseCud<RolesUsers, IdentityContext> rolesUsersCud, IBaseQuery<RolesUsers, IdentityContext> rolesUsersQuery)
    {
        _rolesUsersCud = rolesUsersCud;
        _rolesUsersQuery = rolesUsersQuery;
    }

    public async Task<RolesUsers> CheckAndInsertUserRoleAsync(Guid appUserId, Guid roleId)
    {
        var roleUser = await _rolesUsersQuery.GetAsync(ru => ru.ApplicationsUsersId == appUserId && ru.RoleId == roleId);
        if (roleUser == null)
        {
            roleUser = new()
            {
                RoleId = roleId,
                ApplicationsUsersId = appUserId
            };
            await _rolesUsersCud.InsertAsync(roleUser);
        }
        return roleUser;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
