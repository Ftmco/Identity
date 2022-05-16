namespace Identity.Client.Services;

public class GrpcService : IGrpcRule
{
    readonly IConfiguration _configuration;

    public GrpcService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<Metadata> ApplicationMetaDataAsync()
    {
        string application = _configuration.GetApplicationJson();
        Metadata headers = new();
        headers.Add("application", application);
        return Task.FromResult(headers);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<GrpcChannel> OpenChannelAsync()
    {
        var channelAddress = _configuration["Identity:Address:gRPC"];
        return await OpenChannelAsync(channelAddress);
    }

    public Task<GrpcChannel> OpenChannelAsync(string channelAddress)
    {
        GrpcChannel? channel = GrpcChannel.ForAddress(channelAddress);
        return Task.FromResult(channel);
    }
}
