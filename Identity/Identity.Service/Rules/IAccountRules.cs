namespace Identity.Service.Rules;

public interface IAccountRules
{
    Task<SignUpResponse> SignUpAsync(SignUpViewModel signUp);

    Task<LoginResponse> LoginAsync(LoginViewModel login);

    Task<User> GetUserAsync(HttpContext context);

    Task<User> GetUserFromAuthTokenAsync(Session session);

    Task<User> GetUserAsync(string userName);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<bool> CheckPasswordAsync(string userName, string password);

    Task<ChangePasswordStatus> ChangePasswordAsync(ChangePasswordViewModel changePassword,HttpContext context);

    Task<Application> GetApplicationAsync(ApplicationRequest application);

    Task<IEnumerable<UserViewModel>> CreateUserViewModelAsync(IEnumerable<User> users);

    Task<UserViewModel> CreateUserViewModelAsync(User user);

    Task ForgotPasswordAsync();
}

