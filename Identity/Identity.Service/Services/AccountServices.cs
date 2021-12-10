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

    public AccountServices(IBaseRules<User> userCrud)
    {
        _userCrud = userCrud;
    }

    public Task<ChangePasswordStatus> ChangePasswordAsync(ChangePasswordViewModel changePassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
        => await Task.Run(async () =>
        {
            string? hashPassword = await password.CreateSHA256Async();
            return user.Password == hashPassword;
        });

    public async Task<bool> CheckPasswordAsync(string userName, string password)
    => await Task.Run(async () =>
    {
        User? user = await GetUserAsync(userName);
        return await CheckPasswordAsync(user, password);
    });

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Task<User> GetUserAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserAsync(string userName)
         => await Task.Run(async () =>
             await _userCrud.GetOneAsync(u => u.UserName == userName || u.Email == userName || u.MobileNo == userName));

    public async Task<LoginResponse> LoginAsync(LoginViewModel login)
        => await Task.Run(async () =>
        {
            User? user = await GetUserAsync(login.UserName);
            if (user != null)
            {
                if (await CheckPasswordAsync(user, login.Password))
                {

                }
                return new LoginResponse(LoginStatus.UserNotFound, null);
            }
            return new LoginResponse(LoginStatus.UserNotFound, null);
        });

    public Task<SignUpResponse> SignUpAsync(SignUpViewModel signUp)
    {
        throw new NotImplementedException();
    }
}