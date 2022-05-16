namespace Identity.Service.Abstraction;

public interface IOtpAccountAction : IAsyncDisposable
{
    Task<LoginStatus> OtpLoginAsync(OtpLogin otpLogin, IHeaderDictionary headers);

    Task<LoginResponse> ActivationAsync(Activation activation, IHeaderDictionary headers);
}
