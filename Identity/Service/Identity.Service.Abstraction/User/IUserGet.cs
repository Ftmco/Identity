using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IUserGet : IAsyncDisposable
{
    Task<User?> GetUserAsync(string userName);

    Task<GetUserResponse> GetUserByUserNameAsync(string userName);

    Task<GetUserResponse> GetUserByIdAsync(Guid userId);

    Task<User?> GetUserBySessionAsync(string session);

    Task<User?> GetUserAsync(Guid userId);

    Task<GetUserResponse> FindUserFromSessionAsync(string session);
}
