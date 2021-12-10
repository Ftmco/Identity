using Identity.Entity.User;
namespace Identity.ViewModels.Account.Response;

public record SignUpResponse(SignUpStatus Status, User User);

public record LoginResponse(LoginStatus Status, SessionViewModel? Session);

public record SessionViewModel(string Key, string Value,DateTime ExpireDate);

public enum SignUpStatus
{
    Success = 0,
    UserExist = 1,
    Exception = 2
}

public enum LoginStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3
}

public enum ChangePasswordStatus
{
    Success = 0,
    WrongPassword = 1,
    Exception = 2
}