namespace Identity.Service.Implemention;

public class ApplicationSettingGet : IApplicationSettingGet
{
    readonly IBaseQuery<Setting, IdentityContext> _settingQuery;

    public ApplicationSettingGet(IBaseQuery<Setting, IdentityContext> settingQuery)
    {
        _settingQuery = settingQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Setting?> GetApplicationSettingsAsync(Guid appId)
    {
        IEnumerable<Setting>? settings = await _settingQuery.GetAllAsync(s => s.ApplicationId == appId);
        return settings.FirstOrDefault();
    }
}
