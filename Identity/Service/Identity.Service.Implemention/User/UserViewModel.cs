using Identity.DataBase.Entity;

namespace Identity.Service.Implemention;

public class UserViewModel : IUserViewModel
{
    readonly IBaseQuery<Profile, IdentityContext> _profileQuery;


    public UserViewModel(IBaseQuery<Profile, IdentityContext> profileQuery)
    {
        _profileQuery = profileQuery;
    }

    public async Task<DataBase.ViewModel.UserViewModel> CreateUserViewModelAsync(User user)
    {
        var profile = await _profileQuery.GetAsync(p => p.UserId == user.Id);
        DataBase.ViewModel.UserViewModel viewModel = new(
            Id: user.Id,
            FullName: $"{profile?.FirstName} {profile?.LastName}",
            Email: user.Email,
            MobileNo: user.MobileNo,
            RegisterDate: user.RegisterDate.ToString(),
            IsActive: user.IsActvie);
        return viewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
