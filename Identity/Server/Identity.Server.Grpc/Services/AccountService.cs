using Grpc.Core;
using Identity.DataBase.ViewModel.Account;

namespace Identity.Server.Grpc.Services;

public class AccountService : Account.AccountBase
{
    readonly IAccountAction _account;

    public AccountService(IAccountAction account)
    {
        _account = account;
    }

    public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
    {
        LoginResponse? loginUser = await _account.LoginAsync(new(UserName: request.UserName, Password: request.Password));
        return new LoginReply
        {
            Status = (int)loginUser.Status,
            Session = new() { Key = loginUser.Session?.Key ?? "", Value = loginUser.Session?.Value ?? "" }
        };
    }
}
