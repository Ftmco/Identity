namespace Identity.Service.Rules;

public interface IAccountRules
{
    Task<SignUpResponse> SignUpAsync(SignUpViewModel signUp);

    Task<LoginResponse> LoginAsync(LoginViewModel login);

    Task<ChangePasswordStatus> ChangePasswordAsync(ChangePasswordViewModel changePassword);
}

