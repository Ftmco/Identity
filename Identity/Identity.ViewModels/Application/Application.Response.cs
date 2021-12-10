namespace Identity.ViewModels.Application;

public record GetApplicationsResponse(ActionApplicationsStatus Status, IEnumerable<ApplicationViewModel>? Applications);

public record CUApplicationResponse(CUApplicationStatus Status, ApplicationViewModel Application);

public record DeleteApplicationResponse(ActionApplicationsStatus Status,Guid? Id,string? AppName);

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