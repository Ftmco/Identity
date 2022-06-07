using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IApplicationGet : IAsyncDisposable
{
    Task<Application?> GetApplicationAsync(IHeaderDictionary requestHeader);

    Task<Application?> GetApplicationAsync(string apiKey, string key);

    Task GetApplicationUsersAsync(Guid appId);

    Task GetApplicationUsersAsync(Guid appId, IHeaderDictionary headers);

    Task<IEnumerable<ApplicationInfo>> GetSubApplicationsAsync(Guid appId);

    Task<GetApplicationResponse> GetSubApplicationsAsync(Guid appId, IHeaderDictionary headers);
}

public interface IApplicationSettingGet : IAsyncDisposable
{
    Task<Setting> GetApplicationSettingsAsync(Guid appId);
}