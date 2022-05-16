namespace Identity.Client.Rules;

public interface IOtpAccountAction : IAsyncDisposable
{
    Task<LoginStatus> OtpLoginAsync(OtpRequest otpLogin);

    Task<LoginResponse> OtpActivationAsync(OtpActivationRequest otpActivation);
}
