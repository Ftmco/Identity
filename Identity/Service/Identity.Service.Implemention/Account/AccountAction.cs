using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel.Account;
using Identity.Service.Tools.Code;
using Identity.Service.Tools.Crypto;
using Identity.Service.Tools.Sms;
using Microsoft.AspNetCore.Http;

namespace Identity.Service.Implemention;

public class AccountAction : IAccountAction
{
    readonly IUserGet _userGet;

    readonly ISessionAction _sessionAction;

    readonly IUserAction _userAction;

    readonly IBaseCud<User, IdentityContext> _userCud;

    public AccountAction(IUserGet userGet, ISessionAction sessionAction, IUserAction userAction,
        IBaseCud<User, IdentityContext> userCud)
    {
        _userGet = userGet;
        _sessionAction = sessionAction;
        _userAction = userAction;
        _userCud = userCud;
    }

    public async Task<ActivationStatus> ActivationAsync(Activation activation)
    {
        var user = await _userGet.GetUserByUserNameAsync(activation.UserName);
        if (user != null)
        {
            if (user.ActiveCode == activation.ActiveCode)
            {
                user.IsActvie = true;
                user.ActiveCode = 5.CreateCode();
                user.RegisterDate = DateTime.UtcNow;
                return await _userCud.UpdateAsync(user) ? ActivationStatus.Success : ActivationStatus.Exception;
            }
            return ActivationStatus.WrongActiveCode;
        }
        return ActivationStatus.UserNotFound;
    }

    public async ValueTask DisposeAsync()
    {
        await _userGet.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public Task ForgotPasswrordAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<LoginResponse> LoginAsync(Login login)
    {
        User? user = await _userGet.GetUserByUserNameAsync(login.UserName);
        if (user?.Password == login.Password.CreateSHA256())
        {
            DataBase.Entity.Session? session = await _sessionAction.CreateSessionAsync(user);
            return session != null
                ? new LoginResponse(LoginStatus.Success, new(session.Key, session.Value))
                : new LoginResponse(LoginStatus.Exception, null);
        }
        return new LoginResponse(LoginStatus.UserNotFound, null);
    }

    public async Task LogoutAsync(HttpContext httpContext)
    {
        string session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
            await _sessionAction.DeleteSessionAsync(session);
    }

    public Task ResetForgotPasswordAsync()
    {
        throw new NotImplementedException();
    }

    public Task ResetPasswordAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<SignUpStatus> SignUpAsync(SignUp signUp)
    {
        User? existUser = await _userGet.GetUserByUserNameAsync(signUp.MobileNo);
        if (existUser == null)
        {
            User? user = await _userAction.CreateUserAsync(signUp);
            if (user != null)
            {
                await user.MobileNo.SendSmsAsync(user.ActiveCode);
                return SignUpStatus.Success;
            }
            return SignUpStatus.Exception;
        }
        return SignUpStatus.UserExist;
    }
}
