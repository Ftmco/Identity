using Grpc.Core;
using Identity.DataBase.ViewModel;

namespace Identity.Server.Grpc.Services;

public class UserService : Protos.User.UserBase
{
    readonly IAccountAction _accountAction;

    readonly IProfileGet _profileGet;

    readonly IUserGet _userGet;

    public UserService(IAccountAction accountAction, IUserGet userGet, IProfileGet profileGet)
    {
        _accountAction = accountAction;
        _userGet = userGet;
        _profileGet = profileGet;
    }

    public override async Task<GetUserReply> GetById(GetByIdRequest request, ServerCallContext context)
    {
        var pars = Guid.TryParse(request.Id, out Guid userId);
        if (pars)
        {
            var user = await _userGet.GetUserByIdAsync(userId);
            return user.Status switch
            {
                DataBase.ViewModel.GetUserStatus.Success => new GetUserReply
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
                DataBase.ViewModel.GetUserStatus.UserNotFound => new GetUserReply { Code = 404, Status = false, Title = "کاربر یافت نشد", Message = "", Result = null },
                DataBase.ViewModel.GetUserStatus.Exception => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() },
                _ => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() },
            };
        }
        return new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() };
    }

    public override async Task<GetUserReply> GetBySession(GetBySessionRequest request, ServerCallContext context)
    {
        var user = await _userGet.FindUserFromSessionAsync(request.Session);
        return user.Status switch
        {
            DataBase.ViewModel.GetUserStatus.Success => new GetUserReply
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
            DataBase.ViewModel.GetUserStatus.UserNotFound => new GetUserReply { Code = 404, Status = false, Title = "کاربر یافت نشد", Message = "", Result = null },
            DataBase.ViewModel.GetUserStatus.Exception => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() },
            _ => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() },
        };
    }

    public override async Task<GetUserReply> GetByUserName(GetByUserNameRequest request, ServerCallContext context)
    {
        var user = await _userGet.GetUserByUserNameAsync(request.UserName);
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
            DataBase.ViewModel.GetUserStatus.UserNotFound => new GetUserReply { Code = 404, Status = false, Title = "کاربر یافت نشد", Message = "", Result = null },
            DataBase.ViewModel.GetUserStatus.Exception => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() },
            _ => new GetUserReply() { Code = 500, Title = "خطایی رخ داد مجددا تلاش کنید", Message = "", Status = false, Result = new() }
        };
    }

    public override async Task GetUsers(IAsyncStreamReader<GetByIdRequest> requestStream, IServerStreamWriter<UserModel> responseStream, ServerCallContext context)
    {
        await foreach (GetByIdRequest? rs in requestStream.ReadAllAsync())
        {
            var user = await _userGet.GetUserAsync(rs.Id);
            if (user != null)
            {
                var profile = await _profileGet.GetProfileAsync(user.Id);
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
