namespace Identity.Service.Abstraction;

public interface IApplicationGet : IAsyncDisposable
{
    Task<Application?> GetApplicationAsync(IHeaderDictionary requestHeader);

    Task<Application?> GetApplicationAsync(string apiKey, string key);

}

public interface IApplicationSettingGet : IAsyncDisposable
{
    Task<Setting> GetApplicationSettingsAsync(Guid appId);
}