namespace Identity.Service.Rules;

public interface IApplicationRules
{
    Task<GetApplicationsResponse> GetApplcationsAsync(int page, int count, HttpContext context);

    Task<IEnumerable<ApplicationViewModel>> CreateApplicationViewModelAsync(IEnumerable<Application> applications);

    Task<ApplicationViewModel> CreateApplicationViewModelAsync(Application application);

    Task<CUApplicationResponse> CreateApplicationAsync(CUApplicationViewModel application,HttpContext context);

    Task<DeleteApplicationResponse> DeleteApplicationAsync(DeleteApplicationViewModel delete,HttpContext httpContext);
}
