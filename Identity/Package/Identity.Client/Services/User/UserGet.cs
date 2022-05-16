using User = Identity.Client.Models.User;
using UserRPC = Identity.Server.Grpc.Protos.User;

namespace Identity.Client.Services;

public class UserGet : IUserGet
{
    readonly IConfiguration _configuration;

    readonly ICache _cache;

    readonly IGrpcRule _gRPC;

    private bool _useCache;

    public UserGet(IConfiguration configuration, ICache cache, IGrpcRule gRPC)
    {
        _configuration = configuration;
        _cache = cache;
        _gRPC = gRPC;
        SetSettings();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private void SetSettings()
    {
        _ = bool.TryParse(_configuration["Identity:Cache:Enable"], out _useCache);
    }

    public async Task<User?> GetUserBySessionAsync(string session)
    {
        User? user = null;
        if (_useCache)
        {
            user = await _cache.GetItemAsync<User>(session);
            if (user != null)
                return user;
        }
        GrpcChannel? gRPC = await _gRPC.OpenChannelAsync();
        UserRPC.UserClient client = new(gRPC);
        var getUser = await client.GetBySessionAsync(new() { Session = session });
        if (getUser.Status)
            user = User.CreateUser(getUser.Result);
        if (_useCache)
            await _cache.SetItemAsync(session, user);

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersStreamAsync(IEnumerable<Guid> userIds)
    {
        List<User> usersResult = new();
        await GetUsersStreamAsync(userIds, (user) =>
        {
            usersResult.Add(user);
        });
        return usersResult;
    }

    public async Task GetUsersStreamAsync(IEnumerable<Guid> userIds, Action<User> user)
    {
        var gRPC = await _gRPC.OpenChannelAsync();
        UserRPC.UserClient client = new(gRPC);
        using var call = client.GetUsers();

        Task request = Task.Run(async () =>
        {
            foreach (var userId in userIds)
                await call.RequestStream.WriteAsync(new() { Id = userId.ToString() });
            await call.RequestStream.CompleteAsync();
        });

        Task response = Task.Run(async () =>
        {
            await foreach (UserModel? rs in call.ResponseStream.ReadAllAsync())
            {
                _ = Guid.TryParse(rs.Id, out Guid userId);
                _ = DateTime.TryParse(rs.RegisterDate, out DateTime registerDate);
                User userModel = new()
                {
                    Id = userId,
                    Email = rs.Email,
                    FullName = rs.FullName,
                    IsActive = rs.IsActive,
                    MobileNo = rs.MobileNo,
                    RegisterDate = registerDate
                };
                user(userModel);
            }
        });

        await Task.WhenAll(request, response);
    }

    public async Task<User?> GetUserByUserNameAsync(string username)
    {
        User? user = null;
        if (_useCache)
        {
            user = await _cache.GetItemAsync<User>(username);
            if (user != null)
                return user;
        }
        GrpcChannel? gRPC = await _gRPC.OpenChannelAsync();
        UserRPC.UserClient client = new(gRPC);
        var getUser = await client.GetByUserNameAsync(new() { UserName = username });
        if (getUser.Status)
            user = User.CreateUser(getUser.Result);
        if (_useCache)
            await _cache.SetItemAsync(username, user);

        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        User? user = null;
        if (_useCache)
        {
            user = await _cache.GetItemAsync<User>(userId.ToString());
            if (user != null)
                return user;
        }
        GrpcChannel? gRPC = await _gRPC.OpenChannelAsync();
        UserRPC.UserClient client = new(gRPC);
        var getUser = await client.GetByIdAsync(new() { Id = userId.ToString() });
        if (getUser.Status)
            user = User.CreateUser(getUser.Result);
        if (_useCache)
            await _cache.SetItemAsync(userId.ToString(), user);

        return user;
    }

    public async Task GetUserAvatarsAsync(Guid userId, bool enableBase64, Action<Avatar> avatar)
    {
        var gRPC = await _gRPC.OpenChannelAsync();
        UserRPC.UserClient client = new(gRPC);
        AsyncServerStreamingCall<UserAvatarsReply>? getAvatar = client.GetUserAvatars(new() { EnableBase64 = enableBase64, UserId = userId.ToString() });

        await foreach (UserAvatarsReply? response in getAvatar.ResponseStream.ReadAllAsync())
        {
            _ = Guid.TryParse(response.FileId, out Guid fileId);
            avatar(new(
                FileId: fileId,
                FileToken: response.FileToken,
                Base64: response.Base64));
        }
    }

    public async Task<IEnumerable<Avatar>> GetUserAvatarsAsync(Guid userId, bool enableBase64)
    {
        List<Avatar> avatars = new();
        await GetUserAvatarsAsync(userId, enableBase64, (avatar) =>
            avatars.Add(avatar));

        return avatars;
    }
}
