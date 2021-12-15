using Identity.Tools.File;

namespace Identity.Service.Services;

public class ProfileService : IProfileRules, IDisposable
{

    private readonly IApplicationRules _application;

    private readonly IAccountRules _account;

    private readonly IBaseRules<Profile> _profileCrud;

    public ProfileService(IApplicationRules application, IAccountRules account, IBaseRules<Profile> profileCrud)
    {
        _account = account;
        _application = application;
        _profileCrud = profileCrud;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<GetProfileResponse> GetProfileAsync(ApplicationRequest application, HttpContext httpContext)
        => await Task.Run(async () =>
        {
            Application findApplication = await _application.FindApplicationAsync(application);
            if (findApplication != null)
            {
                User user = await _account.GetUserAsync(httpContext);
                if (user != null)
                {
                    var userViewModel = await _account.CreateUserViewModelAsync(user);
                    Profile profile = await _profileCrud.GetOneAsync(p => p.UserId == user.Id && p.ApplicationId == findApplication.Id);
                    return new GetProfileResponse(GetprofileStatus.Success, (profile != null ?
                            new(profile.Image.CreateFileAddress("profile"), profile.Json, userViewModel)
                                : new("", "", userViewModel)));
                }
                return new GetProfileResponse(GetprofileStatus.UserNotFound, null);
            }
            return new GetProfileResponse(GetprofileStatus.ApplicationNotFoud, null);
        });

    public Task UpdateProfileAsync()
    {
        throw new NotImplementedException();
    }
}