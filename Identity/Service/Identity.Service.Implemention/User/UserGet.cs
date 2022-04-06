using Identity.DataBase.Entity;

namespace Identity.Service.Implemention;

public class UserGet : IUserGet
{
    readonly IBaseQuery<User, IdentityContext> _userQuery;

    public UserGet(IBaseQuery<User, IdentityContext> userQuery)
    {
        _userQuery = userQuery;
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _userQuery.GetAsync(u => u.MobileNo == userName || u.Email == userName || u.UserName == userName);
    }
}
