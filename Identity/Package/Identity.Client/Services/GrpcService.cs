using Grpc.Net.Client;
using Identity.Client.Rules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Client.Services;

public class GrpcService : IGrpcRule
{
    readonly IConfiguration _configuration;

    public GrpcService(IConfiguration configuration)
    {
        _configuration = configuration;
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
