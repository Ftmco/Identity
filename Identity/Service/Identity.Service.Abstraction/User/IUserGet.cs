using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IUserGet : IAsyncDisposable
{
    Task<User?> GetUserByUserNameAsync(string userName);

    Task<User?> GetUserBySessionAsync(string session);

    Task<GetUserFromSessionResponse> FindUserFromSessionAsync(string session);
}
