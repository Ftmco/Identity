namespace Identity.Client.Rules;

public interface IGrpcRule : IAsyncDisposable
{
    Task<GrpcChannel> OpenChannelAsync();

    Task<GrpcChannel> OpenChannelAsync(string channelAddress);

    Task<Metadata> ApplicationMetaDataAsync();
}
