namespace Identity.ViewModels.Application;

public record GetApplicationsResponse(GetApplicationsStatus Status, IEnumerable<ApplicationViewModel>? Applications);

public record CUApplicationResponse(CUApplicationStatus Status, ApplicationViewModel Application);


public enum GetApplicationsStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2
}

public enum CUApplicationStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3,
    ApplicationExist = 4
}