namespace Identity.Service.Abstratcion;

public interface IAccountAction : IAsyncDisposable
{
    Task LoginAsync();

    Task SignUpAsync();

    Task ActivationAsync();

    Task LogoutAsync();

    Task ResetPasswordAsync();

    Task ForgotPasswrordAsync();

    Task ResetForgotPasswordAsync();
}
