using Identity.DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IProfileViewModel : IAsyncDisposable
{
    Task<ProfileViewModel> CreateProfileViewModelAsync(Profile profile);

    Task<IEnumerable<ProfileViewModel>> CreateProfileViewModelAsync(IEnumerable<Profile> profiles);

    Task<IEnumerable<FileViewModel>> CreateProfileImageViewModelAsync(Guid profileId);
}
