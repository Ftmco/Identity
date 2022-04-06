namespace Identity.Service.Abstraction;

public interface IUserGet : IAsyncDisposable
{
    Task<User?> GetUserByUserNameAsync(string userName);
}
