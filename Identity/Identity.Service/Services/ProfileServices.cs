using Identity.Tools.File;

namespace Identity.Service.Services;

public class ProfileService : IProfileRules, IDisposable
{

    private readonly IApplicationRules _application;

    private readonly IAccountRules _account;

    private readonly IBaseRules<Profile> _profileCrud;

    private readonly IBaseRules<User> _userCrud;

    public ProfileService(IApplicationRules application, IAccountRules account,
        IBaseRules<Profile> profileCrud, IBaseRules<User> userCrud)
    {
        _account = account;
        _application = application;
        _profileCrud = profileCrud;
        _userCrud = userCrud;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<ProfileResponse> GetProfileAsync(ApplicationRequest application, HttpContext httpContext)
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
                    return new ProfileResponse(GetprofileStatus.Success, (profile != null ?
                            new(profile.Image.CreateFileAddress("profile"), profile.Json, userViewModel)
                                : new("", "", userViewModel)));
                }
                return new ProfileResponse(GetprofileStatus.UserNotFound, null);
            }
            return new ProfileResponse(GetprofileStatus.ApplicationNotFoud, null);
        });

    public async Task<ProfileResponse> UpdateProfileAsync(UpdateProfileViewModel profile, HttpContext httpContext)
        => await Task.Run(async () =>
        {
            Application findApplication = await _application.FindApplicationAsync(profile.Application);
            if (findApplication != null)
            {
                User user = await _account.GetUserAsync(httpContext);
                if (user != null)
                {
                    Profile userProfile = await _profileCrud.GetOneAsync(p => p.UserId == user.Id && p.ApplicationId == findApplication.Id);
                    if (userProfile == null)
                    {
                        profile.Image.Path = "profile";
                        userProfile = new()
                        {
                            ApplicationId = findApplication.Id,
                            UserId = user.Id,
                            Image = await profile.Image.SaveFileBase64Async(),
                            Json = profile.Json,
                        };
                        await _profileCrud.InsertAsync(userProfile);
                    }
                    user.FullName = profile.User.FullName;
                    return await _userCrud.UpdateAsync(user) ?
                        new ProfileResponse(GetprofileStatus.Success,
                                    new(userProfile.Image.CreateFileAddress("profile"),
                                                    userProfile.Json,
                                                            await _account.CreateUserViewModelAsync(user)))
                            : new ProfileResponse(GetprofileStatus.Exception, null);
                }
                return new ProfileResponse(GetprofileStatus.UserNotFound, null);
            }
            return new ProfileResponse(GetprofileStatus.ApplicationNotFoud, null);
        });
}