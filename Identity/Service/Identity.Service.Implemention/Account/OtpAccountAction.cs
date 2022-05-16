using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel.Account;
using Identity.Service.Tools.Code;
using Identity.Service.Tools.Crypto;
using Identity.Service.Tools.Sms;

namespace Identity.Service.Implemention;

public class OtpAccountAction : IOtpAccountAction
{
    readonly IUserGet _userGet;

    readonly IBaseCud<User, IdentityContext> _userCud;

    readonly ISessionAction _sessionAction;

    public OtpAccountAction(IUserGet userGet, IBaseCud<User, IdentityContext> userCud, ISessionAction sessionAction)
    {
        _userGet = userGet;
        _userCud = userCud;
        _sessionAction = sessionAction;
    }

    public async Task<LoginResponse> ActivationAsync(Activation activation)
    {
        User? user = await _userGet.GetUserAsync(activation.UserName);
        if (user == null || user.ActiveCode != activation.ActiveCode)
            return new LoginResponse(LoginStatus.UserNotFound, null);

        user.ActiveCode = 7.CreateCode();
        user.IsActvie = true;
        await _userCud.UpdateAsync(user);

        var session = await _sessionAction.CreateSessionAsync(user);
        return session != null ?
                new LoginResponse(LoginStatus.Success, new(session.Key, session.Value)) :
                    new LoginResponse(LoginStatus.Exception, null);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<LoginStatus> OtpLoginAsync(FastLogin fastLogin)
    {
        try
        {
            User? user = await _userGet.GetUserAsync(fastLogin.MobileNo);
            var code = 7.CreateCode();
            if (user == null)
            {
                user = new()
                {
                    MobileNo = fastLogin.MobileNo,
                    ActiveCode = code,
                    Email = " ",
                    IsActvie = false,
                    Password = code.CreateSHA256(),
                    RegisterDate = DateTime.UtcNow,
                    UserName = fastLogin.MobileNo
                };
                if (!await _userCud.InsertAsync(user))
                    return LoginStatus.Exception;
            }
            user.ActiveCode = code;
            if (await _userCud.UpdateAsync(user))
            {
                await user.MobileNo.SendSmsAsync(user.ActiveCode);
                return LoginStatus.Success;
            }
            return LoginStatus.Exception;
        }
        catch
        {
            return LoginStatus.Exception;
        }
    }
}
