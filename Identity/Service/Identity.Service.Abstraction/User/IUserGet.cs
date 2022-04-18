using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IUserGet : IAsyncDisposable
{
    Task<User?> GetUserByUserNameAsync(string userName);

    Task<GetUserFromSessionResponse> GetUserFromSessionAsync(string session);
}
