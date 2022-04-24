using Grpc.Core;
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

    readonly IGrpcRule _gRPC;

    public AccountService(IConfiguration configuration, ICache cache, IGrpcRule gRPC)
    {
        _configuration = configuration;
        _cache = cache;
        _gRPC = gRPC;
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
            GrpcChannel? channel = await _gRPC.OpenChannelAsync();
            Account.AccountClient? client = new(channel);
            GetUserReply? user = await client.GetUserAsync(new() { Session = session });
            User? userModel = User.CreateUser(user.Result);
            await _cache.SetItemAsync(session, userModel);
            userCache = userModel;
        }
        return userCache;
    }

    public async Task<IEnumerable<User>> GetUsersStreamAsync(IEnumerable<Guid> userIds)
    {
        GrpcChannel? channel = await _gRPC.OpenChannelAsync();
        Account.AccountClient client = new(channel);
        List<User> usersResult = new();
        using var users = client.GetUsers();

        Task request = Task.Run(async () =>
        {
            foreach (var userid in userIds)
                await users.RequestStream.WriteAsync(new() { Id = userid.ToString() });
            await users.RequestStream.CompleteAsync();
        });

        Task response = Task.Run(async () =>
        {
            await foreach (UserModel? rs in users.ResponseStream.ReadAllAsync())
            {
                _ = Guid.TryParse(rs.Id, out Guid userId);
                _ = DateTime.TryParse(rs.RegisterDate, out DateTime registerDate);
                usersResult.Add(new()
                {
                    Id = userId,
                    Email = rs.Email,
                    FullName = rs.FullName,
                    IsActive = rs.IsActive,
                    MobileNo = rs.MobileNo,
                    RegisterDate = registerDate
                });
            }
        });

        await Task.WhenAll(request, response);
        return usersResult;
    }
}
