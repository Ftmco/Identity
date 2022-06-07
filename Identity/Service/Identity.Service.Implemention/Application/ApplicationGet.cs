using Identity.DataBase.ViewModel;

namespace Identity.Service.Implemention;

public class ApplicationGet : IApplicationGet
{
    readonly IBaseQuery<Application, IdentityContext> _applicationQuery;

    readonly IApplicationViewModel _applicationViewModel;

    public ApplicationGet(IBaseQuery<Application, IdentityContext> applicationQuery, IApplicationViewModel applicationViewModel)
    {
        _applicationQuery = applicationQuery;
        _applicationViewModel = applicationViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Application?> GetApplicationAsync(IHeaderDictionary requestHeader)
    {
        try
        {
            var app = ApplicationViewModel.CreateApplication(requestHeader["application"]);
            return app != null ? await GetApplicationAsync(app.ApiKey, app.Key) : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Application?> GetApplicationAsync(string apiKey, string key)
            => await _applicationQuery.GetAsync(a => a.ApiKey == apiKey && a.Key == key);

    public Task GetApplicationUsersAsync(Guid appId)
    {
        throw new NotImplementedException();
    }

    public Task GetApplicationUsersAsync(Guid appId, IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ApplicationInfo>> GetSubApplicationsAsync(Guid appId)
    {
        var applications = await _applicationQuery.GetAllAsync(a => a.ApplicationId == appId);
        List<ApplicationInfo> applicationsList = new();
        foreach (var app in applications)
        {
            ApplicationInfo? appInfo = await _applicationViewModel.CreateAppInfoViewModelAsync(app);
            appInfo.Childs = await GetSubApplicationsAsync(app.Id);
        }
        return applicationsList;
    }

    public Task<GetApplicationResponse> GetSubApplicationsAsync(Guid appId, IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }
}
