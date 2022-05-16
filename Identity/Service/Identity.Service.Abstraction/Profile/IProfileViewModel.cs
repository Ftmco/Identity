using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IProfileViewModel : IAsyncDisposable
{
    Task<ProfileViewModel> CreateProfileViewModelAsync(Profile profile);

    Task<IEnumerable<ProfileViewModel>> CreateProfileViewModelAsync(IEnumerable<Profile> profiles);

    Task<IEnumerable<FileViewModel>> CreateProfileImageViewModelAsync(Guid profileId);
}
