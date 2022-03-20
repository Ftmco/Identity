namespace Identity.Service.Rules;

public interface IApplicationUserRules : IAsyncDisposable
{
    Task<GetUserResponse> GetUserByTokenAsync(GetUserByToken getUserByToken);

    Task<GetUserResponse> GetUserByUserNameAsync(GetUserByUserName getUserByUserName);
}
