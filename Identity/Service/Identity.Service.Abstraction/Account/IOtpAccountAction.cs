using Grpc.Core;

namespace Identity.Service.Abstraction;

public interface IOtpAccountAction : IAsyncDisposable
{
    Task<LoginStatus> OtpLoginAsync(OtpLogin otpLogin, IHeaderDictionary headers);

    Task<LoginResponse> ActivationAsync(Activation activation, IHeaderDictionary headers);
    
    Task<LoginStatus> OtpLoginAsync(OtpLogin otpLogin, Metadata headers);

    Task<LoginResponse> ActivationAsync(Activation activation, Metadata headers);
}
