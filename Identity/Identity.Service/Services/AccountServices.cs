using Identity.Entity.User;
using Identity.Service.Rules;
using Identity.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Services;

public class AccountServices : IAccountRules, IDisposable
{

    private readonly IBaseRules<User> _userCrud;

    private readonly IBaseRules<Application> _applicationCrud;

    private readonly ISessionRules _session;

    public AccountServices(IBaseRules<User> userCrud, ISessionRules session, IBaseRules<Application> applicationCrud)
    {
        _userCrud = userCrud;
        _session = session;
        _applicationCrud = applicationCrud;
    }

    public Task<ChangePasswordStatus> ChangePasswordAsync(ChangePasswordViewModel changePassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
        => await Task.Run(async () =>
        {
            string hashPassword = await password.CreateSHA256Async();
            return user.Password == hashPassword;
        });

    public async Task<bool> CheckPasswordAsync(string userName, string password)
    => await Task.Run(async () =>
    {
        User user = await GetUserAsync(userName);
        return await CheckPasswordAsync(user, password);
    });

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<User> GetUserAsync(HttpContext context)
     => await Task.Run(async () =>
     {
         Session session = await _session.GetSessionAsync(context);
         return session != null ? await _userCrud.GetOneAsync(u => u.Id == session.UserId) : default;
     });

    public async Task<User> GetUserAsync(string userName)
         => await Task.Run(async () =>
             await _userCrud.GetOneAsync(u => u.UserName == userName || u.Email == userName || u.MobileNo == userName));

    public async Task<LoginResponse> LoginAsync(LoginViewModel login)
        => await Task.Run(async () =>
        {
            Application application = await GetApplicationAsync(login.Application);
            if (application != null)
            {
                User user = await GetUserAsync(login.UserName);
                if (user != null)
                {
                    if (await CheckPasswordAsync(user, login.Password))
                    {
                        Session session = await _session.CreateSessionAsync(user,application);
                        return session != null ?
                            new LoginResponse(LoginStatus.Success, new(session.Key, session.Value, session.ExpireDate)) :
                                new LoginResponse(LoginStatus.Exception, null);
                    }
                    return new LoginResponse(LoginStatus.UserNotFound, null);
                }
                return new LoginResponse(LoginStatus.UserNotFound, null);
            }
            return new LoginResponse(LoginStatus.ApplicationNotFound, null);
        });

    public Task<SignUpResponse> SignUpAsync(SignUpViewModel signUp)
    {
        throw new NotImplementedException();
    }

    public async Task<Application> GetApplicationAsync(ApplicationRequest application)
            => await Task.Run(async () =>
               await _applicationCrud.GetOneAsync(app =>
                   app.ApiKey == application.ApiKey &&
                       app.Password == application.Password.CreateSHA256()));
}