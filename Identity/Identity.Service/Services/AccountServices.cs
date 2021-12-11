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
                User checkUserName = await GetUserAsync(signUp.UserName);
                if (checkUserName != null)
                    return new SignUpResponse(SignUpStatus.UserExist, null);
                User checkMobile = await GetUserAsync(signUp.MobileNo);
                if (checkUserName != null)
                    return new SignUpResponse(SignUpStatus.UserExist, null);
                User checkEmail = await GetUserAsync(signUp.Email);
                if (checkUserName != null)
                    return new SignUpResponse(SignUpStatus.UserExist, null);

                User newUser = CreateUser(signUp);
                return await _userCrud.InsertAsync(newUser) ?
                    new SignUpResponse(SignUpStatus.Success, newUser) :
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
}