using Identity.Entity.User;
using Identity.Service.Rules;
using Identity.Services.Base;

namespace Identity.Service.Services;

public class ApplicationServices : IApplicationRules, IDisposable
{
    private readonly IBaseRules<Application> _applicationCrud;

    private readonly IAccountRules _account;

    public ApplicationServices(IBaseRules<Application> applicationCrud, IAccountRules account)
    {
        _applicationCrud = applicationCrud;
        _account = account;
    }

    public async Task<IEnumerable<ApplicationViewModel>> CreateApplicationViewModelAsync(IEnumerable<Application> applications)
     => await Task.FromResult(applications.Select(app => CreateApplicationViewModelAsync(app).Result));

    public async Task<ApplicationViewModel> CreateApplicationViewModelAsync(Application application)
        => await Task.Run(() => new ApplicationViewModel(application.Id, application.Name, application.Image, application.ApiKey));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<GetApplicationsResponse> GetApplcationsAsync(int page, int count, HttpContext context)
     => await Task.Run(async () =>
     {
         User user = await _account.GetUserAsync(context);
         if (user != null)
         {
             IEnumerable<Application> userApplications = await _applicationCrud.GetAsync(app => app.OwnerId == user.Id);
             IEnumerable<ApplicationViewModel> applicationViewModel = await CreateApplicationViewModelAsync(userApplications);
             return new GetApplicationsResponse(GetApplicationsStatus.Success, applicationViewModel);
         }
         return new GetApplicationsResponse(GetApplicationsStatus.UserNotFound, null);
     });

    public Task GetApplicationAsync(Guid appId, HttpContext context)
    {
        throw new NotImplementedException();
    }

    public Task GetApplicationAsync(string apiKey)
    {
        throw new NotImplementedException();
    }

    public Task GetApplicationAsync(string apiKey, HttpContext context)
    {
        throw new NotImplementedException();
    }

   
}

