using Grpc.Core;
using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel;
using Identity.Server.Grpc.Protos;

namespace Identity.Server.Grpc.Services;

public class AccountService : Account.AccountBase
{
    readonly IAccountAction _accountAction;

    readonly IUserGet _userGet;

    public AccountService(IAccountAction accountAction, IUserGet userGet)
    {
        _accountAction = accountAction;
        _userGet = userGet;
    }

    public override async Task<GetUserReply> GetUser(GetUserRequest request, ServerCallContext context)
    {
        GetUserFromSessionResponse? user = await _userGet.FindUserFromSessionAsync(request.Session);

        return user.Status switch
        {
            GetUserStatus.Success => new GetUserReply
            {
                Code = 200,
                Status = true,
                Message = "مشخاصت کاربر",
                Title = $"{user.User.FullName} مشخصات کاربر",
                Result = new()
                {
                    Email = user.User.Email,
                    FullName = user.User.FullName,
                    Id = user.User.Id.ToString(),
                    IsActive = user.User.IsActive,
                    RegisterDate = user.User.RegisterDate,
                    MobileNo = user.User.MobileNo,
                }
            },
            GetUserStatus.UserNotFound => new GetUserReply { Code = 404, Status = false, Title = "کاربر یافت نشد", Message = "", Result = null },
            GetUserStatus.Exception => new GetUserReply { Code = 500, Status = false, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Result = null },
            _ => new GetUserReply { Code = 500, Status = false, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Result = null },
        };
    }
}
