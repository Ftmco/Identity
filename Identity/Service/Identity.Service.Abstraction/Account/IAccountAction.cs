using Grpc.Core;

namespace Identity.Service.Abstraction;

public interface IAccountAction : IAsyncDisposable
{
    Task<LoginResponse> LoginAsync(Login login,IHeaderDictionary headers);

    Task<SignUpStatus> SignUpAsync(SignUp signUp);

    Task<ActivationStatus> ActivationAsync(Activation activation, IHeaderDictionary headers);

    Task LogoutAsync(HttpContext httpContext);

    Task ResetPasswordAsync();

    Task ForgotPasswrordAsync();

    Task ResetForgotPasswordAsync();

    Task<LoginResponse?> LoginAsync(Login login, Metadata requestHeaders);
}
