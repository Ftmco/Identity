using Identity.ViewModels.Account;

namespace Identity.ViewModels.Application;

public record GetApplicationsResponse(ActionApplicationsStatus Status, IEnumerable<ApplicationViewModel>? Applications);

public record CUApplicationResponse(CUApplicationStatus Status, ApplicationViewModel Application);

public record DeleteApplicationResponse(ActionApplicationsStatus Status, Guid? Id, string? AppName);

public record GetApplicationUsersResponse(GetApplicationUsersStatus Status, IEnumerable<UserViewModel> Users);

public enum GetApplicationUsersStatus
{
    Success = 0,
    ApplicationNotFound = 1,
    Exception = 2
}

public enum ActionApplicationsStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3,
}

public enum CUApplicationStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3,
    ApplicationExist = 4
}