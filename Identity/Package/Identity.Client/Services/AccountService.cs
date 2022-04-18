using Grpc.Net.Client;
using Identity.Client.Cache;
using Identity.Client.Models;
using Identity.Client.Rules;
using Identity.Server.Grpc.Protos;
using Microsoft.Extensions.Configuration;

namespace Identity.Client.Services;

public class AccountService : IAccountRules
{
    readonly IConfiguration _configuration;

    readonly ICache _cache;

    public AccountService(IConfiguration configuration, ICache cache)
    {
        _configuration = configuration;
        _cache = cache;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<User?> GetUserCacheAsync(string session)
    {
        User? userCache = await _cache.GetItemAsync<User>(session);
        if (userCache == null)
        {
            string? channelAddress = _configuration["Identity:Address:gRPC"];
            GrpcChannel? channel = GrpcChannel.ForAddress(channelAddress);
            Account.AccountClient? client = new(channel);
            GetUserReply? user = await client.GetUserAsync(new() { Session = session });
            User? userModel = User.CreateUser(user.Result);
            await _cache.SetItemAsync(session, userModel);
            userCache = userModel;
        }
        return userCache;
    }
}
