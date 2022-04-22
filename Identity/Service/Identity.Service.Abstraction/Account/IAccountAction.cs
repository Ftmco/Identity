using Microsoft.AspNetCore.Http;

namespace Identity.Service.Abstraction;

public interface IAccountAction : IAsyncDisposable
{
    Task<LoginResponse> LoginAsync(Login login);

    Task<SignUpStatus> SignUpAsync(SignUp signUp);

    Task<ActivationStatus> ActivationAsync(Activation activation);

    Task LogoutAsync(HttpContext httpContext);

    Task ResetPasswordAsync();

    Task ForgotPasswrordAsync();

    Task ResetForgotPasswordAsync();
}
