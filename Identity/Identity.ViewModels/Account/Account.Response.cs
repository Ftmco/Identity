namespace Identity.ViewModels.Account.Response;

public record SignUpResponse(SignUpStatus Status, UserViewModel User);

public record LoginResponse(LoginStatus Status, SessionViewModel? Session);

public record SessionViewModel(string Key, string Value, DateTime ExpireDate);

public enum SignUpStatus
{
    Success = 0,
    UserExist = 1,
    Exception = 2,
    ApplicationNotFound = 3
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
    Exception = 2,
    UserNotFound = 3
}

public enum ForgotPasswordStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    WrongCode = 3
}