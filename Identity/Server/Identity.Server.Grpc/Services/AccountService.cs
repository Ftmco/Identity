using Grpc.Core;
using Identity.DataBase.ViewModel.Account;

namespace Identity.Server.Grpc.Services;

public class AccountService : Account.AccountBase
{
    readonly IAccountAction _account;

    readonly IOtpAccountAction _optAccount;

    public AccountService(IAccountAction account, IOtpAccountAction optAccount)
    {
        _account = account;
        _optAccount = optAccount;
    }

    public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
    {
        LoginResponse? loginUser = await _account.LoginAsync(new(UserName: request.UserName, Password: request.Password), context.RequestHeaders);
        return new LoginReply
        {
            Status = (int)loginUser.Status,
            Session = new() { Key = loginUser.Session?.Key ?? "", Value = loginUser.Session?.Value ?? "" }
        };
    }

    public override async Task<OtpReqply> OtpLogin(OtpRequest request, ServerCallContext context)
    {
        var login = await _optAccount.OtpLoginAsync(new(request.MobileNo), context.RequestHeaders);
        return new OtpReqply { Status = (int)login };
    }

    public override async Task<LoginReply> OtpActivation(OtpActivationRequest request, ServerCallContext context)
    {
        var activation = await _optAccount.ActivationAsync(new(request.MobileNo, request.OtpCode), context.RequestHeaders);
        return new LoginReply
        {
            Status = (int)activation.Status,
            Session = new()
            {
                Key = activation.Session?.Key,
                Value = activation.Session?.Value
            }
        };
    }
}
