namespace Identity.Service.Implemention;

public class ApplicationAction : IApplicationAction
{
    readonly IBaseCud<Application, IdentityContext> _applicationCud;

    readonly IBaseCud<ApplicationsUsers, IdentityContext> _appUsersCud;

    readonly IBaseQuery<ApplicationsUsers, IdentityContext> _appUsersQuery;

    readonly IApplicationSettingGet _appSettingGet;

    readonly IRoleGet _roleGet;

    readonly IRoleAction _roleAction;

    public ApplicationAction(IBaseCud<Application, IdentityContext> applicationCud, IBaseCud<ApplicationsUsers, IdentityContext> appUsersCud,
        IBaseQuery<ApplicationsUsers, IdentityContext> appUsersQuery, IApplicationSettingGet appSettingGet,
        IRoleGet roleGet, IRoleAction roleAction)
    {
        _applicationCud = applicationCud;
        _appUsersCud = appUsersCud;
        _appUsersQuery = appUsersQuery;
        _appSettingGet = appSettingGet;
        _roleGet = roleGet;
        _roleAction = roleAction;
    }

    public async Task<ApplicationsUsers> CheckApplicationUserAsync(Guid appId, Guid userId)
    {
        ApplicationsUsers? appUser = await _appUsersQuery.GetAsync(au => au.ApplicationId == appId && au.UserId == userId);
        if (appUser == null)
        {
            appUser = new()
            {
                ApplicationId = appId,
                UserId = userId
            };
            await _appUsersCud.InsertAsync(appUser);
        }
        return appUser;
    }

    public async Task CheckUserDefaultRoleAsync(Guid userId, Guid appId)
    {
        var appUser = await CheckApplicationUserAsync(appId, userId);
        var settings = await _appSettingGet.GetApplicationSettingsAsync(appId);
        await _roleAction.CheckUserRoleAsync(appUser.Id, settings.DefaultRole);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
