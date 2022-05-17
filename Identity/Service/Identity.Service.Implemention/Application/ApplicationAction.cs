using Identity.DataBase.ViewModel;
using Identity.Service.Tools.Code;
using Identity.Service.Tools.Crypto;
using static Identity.Service.Tools.Crypto.CryptoEngine;

namespace Identity.Service.Implemention;

public class ApplicationAction : IApplicationAction
{
    readonly IBaseCud<Application, IdentityContext> _applicationCud;

    readonly IBaseCud<Setting, IdentityContext> _settingCud;

    readonly IBaseQuery<Application, IdentityContext> _applicationQuery;

    readonly IBaseCud<ApplicationsUsers, IdentityContext> _appUsersCud;

    readonly IBaseQuery<ApplicationsUsers, IdentityContext> _appUsersQuery;

    readonly IApplicationSettingGet _appSettingGet;

    readonly IRoleGet _roleGet;

    readonly IRoleAction _roleAction;

    readonly IUserGet _userGet;

    public ApplicationAction(IBaseCud<Application, IdentityContext> applicationCud, IBaseCud<ApplicationsUsers, IdentityContext> appUsersCud,
        IBaseQuery<ApplicationsUsers, IdentityContext> appUsersQuery, IApplicationSettingGet appSettingGet,
        IRoleGet roleGet, IRoleAction roleAction, IBaseQuery<Application, IdentityContext> applicationQuery, IUserGet userGet,
        IBaseCud<Setting, IdentityContext> settingCud)
    {
        _applicationCud = applicationCud;
        _appUsersCud = appUsersCud;
        _appUsersQuery = appUsersQuery;
        _appSettingGet = appSettingGet;
        _roleGet = roleGet;
        _roleAction = roleAction;
        _applicationQuery = applicationQuery;
        _userGet = userGet;
        _settingCud = settingCud;
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

    public async Task<UpsertApplicationResponse> UpsertAsync(UpsertApplication upsert, IHeaderDictionary headers)
    {
        var user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"]);
        if (user != null)
        {
            Application? app = await _applicationQuery.GetAsync(upsert.Id);
            if (app == null)
            {
                string? code = 16.CreateCode();
                string? key = Encrypt(upsert.Name, code);
                app = new()
                {
                    Code = code,
                    ApiKey = Guid.NewGuid().ToString().CreateSHA256(),
                    Key = key,
                    CreateDate = DateTime.UtcNow,
                    IsActive = false,
                    Name = upsert.Name,
                    OwnerId = user.Id,
                };
                return await _applicationCud.InsertAsync(app) ?
                        new UpsertApplicationResponse(ApplicationActionStatus.Success, null) :
                            new UpsertApplicationResponse(ApplicationActionStatus.Exception, null);
            }
            app.Name = upsert.Name;
            return await _applicationCud.UpdateAsync(app) ?
                       new UpsertApplicationResponse(ApplicationActionStatus.Success, null) :
                           new UpsertApplicationResponse(ApplicationActionStatus.Exception, null);
        }
        return new UpsertApplicationResponse(ApplicationActionStatus.UserNotFound, null);
    }
}
