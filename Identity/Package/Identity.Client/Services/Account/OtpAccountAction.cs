namespace Identity.Client.Services;

public class OtpAccountAction : IOtpAccountAction
{
    readonly IGrpcRule _gRPC;

    public OtpAccountAction(IGrpcRule gRPC)
    {
        _gRPC = gRPC;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _gRPC.DisposeAsync();
    }

    public async Task<LoginResponse> OtpActivationAsync(OtpActivationRequest otpActivation)
    {
        Metadata? metaData = await _gRPC.ApplicationMetaDataAsync();
        var channel = await _gRPC.OpenChannelAsync();
        Account.AccountClient client = new(channel);
        LoginReply? request = await client.OtpActivationAsync(otpActivation, metaData);
        return new LoginResponse((LoginStatus)request.Status, new(Key: request.Session.Key,
                                                                Value: request.Session.Value));
    }

    public async Task<LoginStatus> OtpLoginAsync(OtpRequest otpLogin)
    {
        Metadata? metaData = await _gRPC.ApplicationMetaDataAsync();
        var channel = await _gRPC.OpenChannelAsync();
        Account.AccountClient client = new(channel);
        OtpReqply? request = await client.OtpLoginAsync(otpLogin, metaData);
        return (LoginStatus)request.Status;
    }
}
