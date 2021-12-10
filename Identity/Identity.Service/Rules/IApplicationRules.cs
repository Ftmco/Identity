namespace Identity.Service.Rules;

public interface IApplicationRules
{
    Task<GetApplicationsResponse> GetApplcationsAsync(int page, int count, HttpContext context);

    Task<IEnumerable<ApplicationViewModel>> CreateApplicationViewModelAsync(IEnumerable<Application> applications);

    Task<ApplicationViewModel> CreateApplicationViewModelAsync(Application application);

    Task GetApplicationAsync(Guid appId,HttpContext context);

    Task GetApplicationAsync(string apiKey);

    Task GetApplicationAsync(string apiKey,HttpContext context);
}
