using Identity.DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public class ProfileGet : IProfileGet
{
    readonly IBaseQuery<Profile, IdentityContext> _profileQuery;

    readonly IUserGet _userGet;

    readonly IProfileViewModel _profileViewModel;

    public ProfileGet(IBaseQuery<Profile, IdentityContext> profileQuery, IUserGet userGet, IProfileViewModel profileViewModel)
    {
        _profileQuery = profileQuery;
        _userGet = userGet;
        _profileViewModel = profileViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<ProfileResponse> GetProfileAsync(HttpContext httpContext)
    {
        string session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            User? user = await _userGet.GetUserBySessionAsync(session);
            return user != null ?
                await GetProfileAsync(user.Id) 
                    : new ProfileResponse(ProfileStatus.NotAuthorized, null);
        }
        return new ProfileResponse(ProfileStatus.NotAuthorized, null);
    }

    public async Task<ProfileResponse> GetProfileAsync(Guid userId)
    {
        Profile? profile = await _profileQuery.GetAsync(p => p.UserId == userId);
        if (profile != null)
        {
            var profileViewModel = await _profileViewModel.CreateProfileViewModelAsync(profile);
            return new ProfileResponse(ProfileStatus.Success, profileViewModel);
        }
        return new ProfileResponse(ProfileStatus.NotFound, null);
    }
}
