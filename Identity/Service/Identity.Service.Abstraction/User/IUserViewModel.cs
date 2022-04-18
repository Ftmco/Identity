using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IUserViewModel : IAsyncDisposable
{
    Task<UserViewModel> CreateUserViewModelAsync(User user);
}
