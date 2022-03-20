using Identity.Tools.Code;

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

    public async Task<ChangePasswordStatus> ChangePasswordAsync(ChangePasswordViewModel changePassword, HttpContext context)
        => await Task.Run(async () =>
        {
            User user = await GetUserAsync(context);
            if (user != null)
            {
                if (await CheckPasswordAsync(user, changePassword.CurrentPassword))
                {
                    user.Password = await changePassword.NewPassword.CreateSHA256Async();
                    return await _userCrud.UpdateAsync(user) ?
                        ChangePasswordStatus.Success : ChangePasswordStatus.Exception;
                }
                return ChangePasswordStatus.WrongPassword;
            }
            return ChangePasswordStatus.UserNotFound;
        });

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
         return await GetUserFromAuthTokenAsync(session);
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
                        Session session = await _session.CreateSessionAsync(user, application);
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

    public async Task<SignUpResponse> SignUpAsync(SignUpViewModel signUp)
            => await Task.Run(async () =>
            {
                bool userExist = await _userCrud.AnyAsync(u => u.UserName == signUp.UserName
                                                          || u.Email == signUp.Email
                                                              || u.MobileNo == signUp.MobileNo);
                if (userExist)
                    return new SignUpResponse(SignUpStatus.UserExist, null);

                User newUser = CreateUser(signUp);
                return await _userCrud.InsertAsync(newUser) ?
                    new SignUpResponse(SignUpStatus.Success, await CreateUserViewModelAsync(newUser)) :
                        new SignUpResponse(SignUpStatus.Exception, null);
            });

    public async Task<Application> GetApplicationAsync(ApplicationRequest application)
            => await Task.Run(async () =>
               await _applicationCrud.GetOneAsync(app =>
                   app.ApiKey == application.ApiKey &&
                       app.Password == application.Password.CreateSHA256()));

    private static User CreateUser(SignUpViewModel signUp)
        => new()
        {
            ActiveCode = 6.CreateCode(),
            Email = signUp.Email,
            FullName = signUp.FullName,
            IsActive = false,
            MobileNo = signUp.MobileNo,
            Password = signUp.Password.CreateSHA256(),
            RegisterDate = DateTime.Now,
            UserName = signUp.UserName
        };

    public async Task<User> GetUserFromAuthTokenAsync(Session session)
        => await Task.Run(async () =>
            session != null ? await _userCrud.GetOneAsync(u => u.Id == session.UserId) : default);

    public async Task<IEnumerable<UserViewModel>> CreateUserViewModelAsync(IEnumerable<User> users)
        => await Task.Run(() => users.Select(u => CreateUserViewModelAsync(u).Result));

    public async Task<UserViewModel> CreateUserViewModelAsync(User user)
            => await Task.Run(() => new UserViewModel(
                Id: user.Id,
                UserName: user.UserName,
                FullName: user.FullName,
                Email: user.Email,
                MobileNo: user.MobileNo,
                IsActive: user.IsActive,
                RegisterDate: user.RegisterDate
                ));

    public async Task<ForgotPasswordStatus> ForgotPasswordAsync(ForgotPasswordViewModel forgotPassword)
        => await Task.Run(async () =>
        {
            User user = await GetUserAsync(forgotPassword.UserName);
            if (user == null)
                return ForgotPasswordStatus.UserNotFound;

            user.ActiveCode = 7.CreateCode();
            return await _userCrud.UpdateAsync(user) ?
                    ForgotPasswordStatus.Success
                        : ForgotPasswordStatus.Exception;
        });

    public async Task<ForgotPasswordStatus> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
            => await Task.Run(async () =>
            {
                User user = await GetUserAsync(resetPassword.UserName);
                if (user == null)
                    return ForgotPasswordStatus.UserNotFound;

                if (user.ActiveCode != resetPassword.Code)
                    return ForgotPasswordStatus.WrongCode;

                user.Password = await resetPassword.Password.CreateSHA256Async();

                return await _userCrud.UpdateAsync(user) ?
                        ForgotPasswordStatus.Success
                            : ForgotPasswordStatus.Exception;
            });
}