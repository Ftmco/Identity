using Identity.DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public class ProfileViewModel : IProfileViewModel
{
    readonly IBaseQuery<ProfileImage, IdentityContext> _profileImageQuery;

    readonly IBaseQuery<User, IdentityContext> _userQuery;

    readonly IUserViewModel _userViewModel;

    public ProfileViewModel(IBaseQuery<ProfileImage, IdentityContext> profileImageQuery,
        IUserViewModel userViewModel, IBaseQuery<User, IdentityContext> userQuery)
    {
        _profileImageQuery = profileImageQuery;
        _userViewModel = userViewModel;
        _userQuery = userQuery;
    }

    public async Task<DataBase.ViewModel.ProfileViewModel> CreateProfileViewModelAsync(Profile profile)
    {
        IEnumerable<ProfileImage>? images = await _profileImageQuery.GetAllAsync(pi => pi.ProfileId == profile.Id);
        User? user = await _userQuery.GetAsync(profile.UserId);
        DataBase.ViewModel.ProfileViewModel profileViewModel = new(
            ProfileId: profile.Id,
            UserId: profile.UserId,
            FirstName: profile.FirstName,
            LastName: profile.LastName,
            User: await _userViewModel.CreateUserViewModelAsync(user),
            Images: images.Select(pi => new FileViewModel(FileId: pi.Id, FileToken: pi.FileToken)));
        return profileViewModel;
    }

    public async Task<IEnumerable<DataBase.ViewModel.ProfileViewModel>> CreateProfileViewModelAsync(IEnumerable<Profile> profiles)
    {
        List<DataBase.ViewModel.ProfileViewModel> profileViewModels = new();
        foreach (var profile in profiles)
            profileViewModels.Add(await CreateProfileViewModelAsync(profile));
        return profileViewModels;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
