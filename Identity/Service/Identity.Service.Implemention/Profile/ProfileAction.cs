using Identity.DataBase.ViewModel;

namespace Identity.Service.Implemention;

public class ProfileAction : IProfileAction
{
    readonly IUserGet _userGet;

    readonly IUserAction _userAction;

    readonly IProfileViewModel _profileViewModel;

    readonly IBaseQuery<Profile, IdentityContext> _profileQuery;

    readonly IBaseCud<Profile, IdentityContext> _profileCud;

    readonly IBaseCud<ProfileImage, IdentityContext> _profileImageCud;

    public ProfileAction(IUserGet userGet, IBaseQuery<Profile, IdentityContext> profileQuery,
        IBaseCud<Profile, IdentityContext> profileCud, IBaseCud<ProfileImage, IdentityContext> profileImageCud,
        IProfileViewModel profileViewModel, IUserAction userAction)
    {
        _userGet = userGet;
        _profileQuery = profileQuery;
        _profileCud = profileCud;
        _profileImageCud = profileImageCud;
        _profileViewModel = profileViewModel;
        _userAction = userAction;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<ProfileResponse> UpdateProfileAsync(UpdateProfile updateProfile, HttpContext httpContext)
    {
        string session = httpContext.Request.Headers["Auth-Token"];
        var user = await _userGet.GetUserBySessionAsync(session);
        if (user != null)
        {
            Profile? profile = await _profileQuery.GetAsync(p => p.UserId == user.Id);
            if (profile == null)
            {
                profile = new()
                {
                    UserId = user.Id,
                    FirstName = updateProfile.FirstName,
                    LastName = updateProfile.LastName
                };
                await _profileCud.InsertAsync(profile);
            }
            profile.FirstName = updateProfile.FirstName;
            profile.LastName = updateProfile.LastName;
            await _userAction.UpdateUserEmailAsync(user, updateProfile.Email);
            if (await _profileCud.UpdateAsync(profile))
            {
                if (updateProfile.FileId != null)
                {
                    ProfileImage image = new()
                    {
                        FileId = updateProfile.FileId.Value,
                        FileToken = updateProfile.FileToken,
                        ProfileId = profile.Id
                    };
                    await _profileImageCud.InsertAsync(image);
                }
                return new ProfileResponse(ProfileStatus.Success, await _profileViewModel.CreateProfileViewModelAsync(profile));
            }
            return new ProfileResponse(ProfileStatus.Exception, null);
        }
        return new ProfileResponse(ProfileStatus.NotAuthorized, null);
    }
}
