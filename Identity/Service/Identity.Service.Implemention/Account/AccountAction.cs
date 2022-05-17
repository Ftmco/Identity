using Grpc.Core;
using Identity.DataBase.ViewModel.Account;
using Identity.Service.Tools;
using Identity.Service.Tools.Code;
using Identity.Service.Tools.Crypto;
using Identity.Service.Tools.Sms;
using Microsoft.Extensions.Primitives;

namespace Identity.Service.Implemention;

public class AccountAction : IAccountAction
{
    readonly IUserGet _userGet;

    readonly ISessionAction _sessionAction;

    readonly IUserAction _userAction;

    readonly IBaseCud<User, IdentityContext> _userCud;

    readonly IApplicationGet _applicationGet;

    public AccountAction(IUserGet userGet, ISessionAction sessionAction, IUserAction userAction,
        IBaseCud<User, IdentityContext> userCud, IApplicationGet applicationGet)
    {
        _userGet = userGet;
        _sessionAction = sessionAction;
        _userAction = userAction;
        _userCud = userCud;
        _applicationGet = applicationGet;
    }

    public async Task<ActivationStatus> ActivationAsync(Activation activation, IHeaderDictionary headers)
    {
        var user = await _userGet.GetUserAsync(activation.UserName);
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

    public async Task<LoginResponse> LoginAsync(Login login, IHeaderDictionary headers)
    {
        var app = await _applicationGet.GetApplicationAsync(headers);
        if (app != null)
        {
            User? user = await _userGet.GetUserAsync(login.UserName);
            if (user?.Password == login.Password.CreateSHA256())
            {
                DataBase.Entity.Session? session = await _sessionAction.CreateSessionAsync(user, app.Id);
                return session != null
                    ? new LoginResponse(LoginStatus.Success, new(session.Key, session.Value))
                    : new LoginResponse(LoginStatus.Exception, null);
            }
            return new LoginResponse(LoginStatus.UserNotFound, null);
        }
        return new LoginResponse(LoginStatus.ApplicationNotfound, null);
    }

    public async Task<LoginResponse?> LoginAsync(Login login, Metadata requestHeaders)
        => await LoginAsync(login, requestHeaders.ConvertToHeaderDictonary());

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
        User? existUser = await _userGet.GetUserAsync(signUp.MobileNo);
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
