namespace Identity.ViewModels.Account;

public record GetProfileResponse(GetprofileStatus Status,ProfileViewModel Profile);

public enum GetprofileStatus
{
    Success = 0,
    ApplicationNotFoud =1,
    Exception = 2,
    UserNotFound = 3,
    EmptyProfile = 4
}