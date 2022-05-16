using Identity.DataBase.ViewModel;

namespace Identity.Service.Implemention;

public class ApplicationGet : IApplicationGet
{
    readonly IBaseQuery<Application, IdentityContext> _applicationQuery;

    public ApplicationGet(IBaseQuery<Application, IdentityContext> applicationQuery)
    {
        _applicationQuery = applicationQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Application?> GetApplicationAsync(IHeaderDictionary requestHeader)
    {
        var app = ApplicationViewModel.CreateApplication(requestHeader["application"]);
        return app != null ? await GetApplicationAsync(app.ApiKey, app.Key) : null;
    }

    public async Task<Application?> GetApplicationAsync(string apiKey, string key)
            => await _applicationQuery.GetAsync(a => a.ApiKey == apiKey && a.Key == key);
}
