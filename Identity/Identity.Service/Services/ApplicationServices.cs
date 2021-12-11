using Identity.Entity.User;
using Identity.Service.Rules;
using Identity.Services.Base;
using Identity.Tools.File;
using Identity.ViewModels.File;

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

    public async Task<CUApplicationResponse> CreateApplicationAsync(CUApplicationViewModel application, HttpContext context)
        => await Task.Run(async () =>
        {
            User user = await _account.GetUserAsync(context);
            if (user != null)
            {
                IEnumerable<Application> userApplications = await _applicationCrud.GetAsync(app => app.OwnerId == user.Id);
                if (!userApplications.Any(ua => ua.Name == application.Name))
                {
                    Application newApplication = await CreateApplicationAsync(application, user);
                    return await _applicationCrud.InsertAsync(newApplication) ?
                        new CUApplicationResponse(CUApplicationStatus.Success, await CreateApplicationViewModelAsync(newApplication))
                            : new CUApplicationResponse(CUApplicationStatus.Exception, null);
                }
                return new CUApplicationResponse(CUApplicationStatus.ApplicationExist, null);
            }
            return new CUApplicationResponse(CUApplicationStatus.UserNotFound, null);
        });

    public async Task<IEnumerable<ApplicationViewModel>> CreateApplicationViewModelAsync(IEnumerable<Application> applications)
     => await Task.FromResult(applications.Select(app => CreateApplicationViewModelAsync(app).Result));

    public async Task<ApplicationViewModel> CreateApplicationViewModelAsync(Application application)
        => await Task.Run(() => new ApplicationViewModel(application.Id, application.Name, application.Image.CreateFileAddress("application"), application.ApiKey));

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
             return new GetApplicationsResponse(ActionApplicationsStatus.Success, applicationViewModel);
         }
         return new GetApplicationsResponse(ActionApplicationsStatus.UserNotFound, null);
     });

    public Task<GetApplicationsResponse> GetApplcationsAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

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

    private static async Task<Application> CreateApplicationAsync(CUApplicationViewModel cuApp, User user)
        => await Task.Run(async () => new Application()
        {
            ApiKey = Guid.NewGuid().ToString().CreateSHA256(),
            Password = cuApp.Password.CreateSHA256(),
            OwnerId = user.Id,
            Name = cuApp.Name,
            Image = await SaveAppProfileAsync(cuApp.File)
        });

    private static async Task<string> SaveAppProfileAsync(FileBase64ViewModel fileBase64)
        => await Task.Run(async () =>
        {
            string img = "null.png";
            if (fileBase64 != null)
            {
                fileBase64.Path = "application";
                img = await fileBase64.SaveFileBase64Async();
            }
            return img;
        });

    public async Task<DeleteApplicationResponse> DeleteApplicationAsync(DeleteApplicationViewModel delete, HttpContext httpContext)
        => await Task.Run(async () =>
        {
            User user = await _account.GetUserAsync(httpContext);
            if (user != null)
            {
                Application application = await _applicationCrud.GetOneAsync(app => app.Id == delete.Id && app.OwnerId == user.Id);
                if (application != null)
                {
                    if (await _applicationCrud.DeleteAsync(application))
                    {
                        _ = await application.Image.DeleteFileAsync("application");
                        return new DeleteApplicationResponse(ActionApplicationsStatus.Success, application.Id, application.Name);
                    }
                    return new DeleteApplicationResponse(ActionApplicationsStatus.Exception, null, null);
                }
                return new DeleteApplicationResponse(ActionApplicationsStatus.ApplicationNotFound, null, null);
            }
            return new DeleteApplicationResponse(ActionApplicationsStatus.UserNotFound, null, null);
        });

    public async Task<CUApplicationResponse> UpdateApplicationAsync(CUApplicationViewModel application, HttpContext context)
            => await Task.Run(async () =>
            {
                User user = await _account.GetUserAsync(context);
                if (user != null)
                {
                    Application app = await _applicationCrud.GetOneAsync(application.Id);
                    if (app != null)
                    {
                        if (application.File != null)
                        {
                            await app.Image.DeleteFileAsync("application");
                            app.Image = await SaveAppProfileAsync(application.File);
                        }

                        app.Name = application.Name;
                        app.Password = !string.IsNullOrEmpty(application.Password) ?
                            await application.Password.CreateSHA256Async() :
                                    app.Password;

                        return await _applicationCrud.UpdateAsync(app) ?
                                new CUApplicationResponse(CUApplicationStatus.Success, await CreateApplicationViewModelAsync(app)) :
                                        new CUApplicationResponse(CUApplicationStatus.Exception, null);
                    }
                    return new CUApplicationResponse(CUApplicationStatus.ApplicationNotFound, null);
                }
                return new CUApplicationResponse(CUApplicationStatus.UserNotFound, null);
            });
}

