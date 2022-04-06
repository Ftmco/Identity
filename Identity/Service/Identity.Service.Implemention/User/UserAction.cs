using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel.Account;
using Identity.Service.Tools.Code;
using Identity.Service.Tools.Crypto;

namespace Identity.Service.Implemention;

public class UserAction : IUserAction
{
    readonly IBaseCud<User, IdentityContext> _userCud;

    readonly IBaseQuery<User, IdentityContext> _userQuery;

    readonly IUserGet _userGet;

    public UserAction(IBaseCud<User, IdentityContext> userCud, IBaseQuery<User, IdentityContext> userQuery, IUserGet userGet)
    {
        _userCud = userCud;
        _userQuery = userQuery;
        _userGet = userGet;
    }

    public async Task<User?> CreateUserAsync(SignUp signUp)
    {
        User newUser = new()
        {
            Email = " ",
            FullName = signUp.FullName,
            MobileNo = signUp.MobileNo,
            IsActvie = false,
            Password = signUp.Password.CreateSHA256(),
            RegisterDate = DateTime.UtcNow,
            UserName = " ",
            ActiveCode = 5.CreateCode()
        };
        return await _userCud.InsertAsync(newUser) ? newUser : null;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _userGet.DisposeAsync();
    }
}
