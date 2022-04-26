namespace Identity.DataBase.ViewModel;

public record UserViewModel(Guid Id, string FullName, string Email, string MobileNo, string RegisterDate, bool IsActive);

public record GetUserResponse(GetUserStatus Status, UserViewModel? User);

public enum GetUserStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2
}