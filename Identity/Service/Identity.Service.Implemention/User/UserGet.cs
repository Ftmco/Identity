using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel;

namespace Identity.Service.Implemention;

public class UserGet : IUserGet
{
    readonly IBaseQuery<User, IdentityContext> _userQuery;

    readonly ISessionGet _sessionGet;

    readonly IUserViewModel _userViewModel;

    public UserGet(IBaseQuery<User, IdentityContext> userQuery, ISessionGet sessionGet, IUserViewModel userViewModel)
    {
        _userQuery = userQuery;
        _sessionGet = sessionGet;
        _userViewModel = userViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _userQuery.GetAsync(u => u.MobileNo == userName || u.Email == userName || u.UserName == userName);
    }

    public async Task<GetUserFromSessionResponse> FindUserFromSessionAsync(string session)
    {
        var user = await GetUserBySessionAsync(session);
        return user != null
            ? new GetUserFromSessionResponse(GetUserStatus.Success, await _userViewModel.CreateUserViewModelAsync(user))
            : new GetUserFromSessionResponse(GetUserStatus.UserNotFound, null);
    }

    public async Task<User?> GetUserBySessionAsync(string session)
    {
        Session? userSession = await _sessionGet.GetSessionAsync(session);
        return userSession != null ? await _userQuery.GetAsync(userSession.UserId) : null;
    }

    public async Task<User?> GetUserAsync(string userId)
    {
        return Guid.TryParse(userId, out Guid uId)
            ? await _userQuery.GetAsync(uId) : null;
    }
}
