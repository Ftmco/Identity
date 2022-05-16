namespace Identity.Client.Services;

public class AccountAction : IAccountAction
{
    readonly IGrpcRule _gRPC;

    public AccountAction(IGrpcRule gRPC)
    {
        _gRPC = gRPC;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<LoginReply> LoginAsync(LoginRequest login)
    {
        GrpcChannel? grpc = await _gRPC.OpenChannelAsync();
        Account.AccountClient? client = new(grpc);
        LoginReply? request = await client.LoginAsync(login);
        return request;
    }
}
