using Identity.DataBase.ViewModel;

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

    public async Task<IEnumerable<FileViewModel>> CreateProfileImageViewModelAsync(Guid profileId)
    {
        IEnumerable<ProfileImage>? images = await _profileImageQuery.GetAllAsync(pi => pi.ProfileId == profileId);
        if (images == null)
            return new List<FileViewModel>();
        return images.Select(pi => new FileViewModel(FileId: pi.Id, FileToken: pi.FileToken));
    }

    public async Task<DataBase.ViewModel.ProfileViewModel> CreateProfileViewModelAsync(Profile profile)
    {
        User? user = await _userQuery.GetAsync(profile.UserId);
        DataBase.ViewModel.ProfileViewModel profileViewModel = new(
            ProfileId: profile.Id,
            UserId: profile.UserId,
            FirstName: profile.FirstName,
            LastName: profile.LastName,
            User: await _userViewModel.CreateUserViewModelAsync(user),
            Images: await CreateProfileImageViewModelAsync(profile.Id));
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
