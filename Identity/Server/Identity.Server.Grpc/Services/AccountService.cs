using Grpc.Core;
using Identity.DataBase.Entity;
using Identity.DataBase.ViewModel;
using Identity.Server.Grpc.Protos;

namespace Identity.Server.Grpc.Services;

public class AccountService : Account.AccountBase
{
    readonly IAccountAction _accountAction;

    readonly IProfileGet _profileGet;

    readonly IUserGet _userGet;

    public AccountService(IAccountAction accountAction, IUserGet userGet, IProfileGet profileGet)
    {
        _accountAction = accountAction;
        _userGet = userGet;
        _profileGet = profileGet;
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

    public override async Task GetUsers(IAsyncStreamReader<GetUserById> requestStream, IServerStreamWriter<UserModel> responseStream, ServerCallContext context)
    {
        await foreach (GetUserById? rs in requestStream.ReadAllAsync())
        {
            User? user = await _userGet.GetUserAsync(rs.Id);
            if (user != null)
            {
                ProfileResponse? profile = await _profileGet.GetProfileAsync(user.Id);
                await responseStream.WriteAsync(new()
                {
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    FullName = profile.Status == ProfileStatus.Success ? $"{profile.Profile.FirstName} {profile.Profile.LastName}" : "",
                    IsActive = user.IsActvie,
                    MobileNo = user.MobileNo,
                    RegisterDate = user.RegisterDate.ToString()
                });
            }
        }
    }
}
