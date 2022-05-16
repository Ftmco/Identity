namespace Identity.Client.Rules;

public interface IAccountAction : IAsyncDisposable
{
    Task<LoginReply> LoginAsync(LoginRequest login);
}
