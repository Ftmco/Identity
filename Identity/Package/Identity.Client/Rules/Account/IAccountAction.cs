using Identity.Client.Models;
using Identity.Server.Grpc.Protos;

namespace Identity.Client.Rules;

public interface IAccountAction : IAsyncDisposable
{
    Task<LoginReply> LoginAsync(LoginRequest login);
}
