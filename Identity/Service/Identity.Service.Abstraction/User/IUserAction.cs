namespace Identity.Service.Abstraction;

public interface IUserAction : IAsyncDisposable
{
    Task<User?> CreateUserAsync(SignUp signUp);

    Task UpdateUserEmailAsync(User user,string email);
}
