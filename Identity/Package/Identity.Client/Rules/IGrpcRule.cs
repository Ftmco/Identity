using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Client.Rules;

public interface IGrpcRule : IAsyncDisposable
{
    Task<GrpcChannel> OpenChannelAsync();

    Task<GrpcChannel> OpenChannelAsync(string channelAddress);
}
