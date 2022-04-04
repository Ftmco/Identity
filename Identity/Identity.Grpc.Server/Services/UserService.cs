using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Identity.Grpc.Server.Protos;
using Identity.Service.Rules;
using Identity.ViewModels.Application;

namespace Identity.Grpc.Server.Services;

public class UserService : IdentityService.IdentityServiceBase
{
    readonly IApplicationUserRules _applicationUser;

    public UserService(IApplicationUserRules applicationUser)
    {
        _applicationUser = applicationUser;
    }

    public override async Task<FindUserResponse> GetUser(FindUser request, ServerCallContext context)
    {
        ApplicationRequest application = new(
            ApiKey: context.RequestHeaders.GetValue("apiKey"),
            Password: context.RequestHeaders.GetValue("password"));
        GetUserResponse? getUser = await _applicationUser.GetUserByTokenAsync(new(request.Token) { Application = application });
        return new FindUserResponse()
        {
            Status = (int)getUser.Status,
            User = new()
            {
                ActiveDate = Timestamp.FromDateTimeOffset(getUser.User.RegisterDate),
                Email = getUser.User.Email,
                FullName = getUser.User.FullName,
                Id = getUser.User.Id.ToString(),
                IsActive = getUser.User.IsActive,
                PhoneNumber = getUser.User.MobileNo
            },
            Message = getUser.Status switch
            {
                GetUserStatus.Success => "عملیات موفقیت آمیز بود",
                GetUserStatus.ApplicationNotFound => "اطلاعات اپلیکیشن اشتباه است",
                GetUserStatus.Exception => "خطایی رخ داد",
                GetUserStatus.UserNotFound => "کاربری یافت نشد",
                _ => "خطایی رخ داد",
            }
        };
    }

}
