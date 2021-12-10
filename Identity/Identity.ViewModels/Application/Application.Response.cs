namespace Identity.ViewModels.Application;

public record GetApplicationsResponse(GetApplicationsStatus Status,IEnumerable<ApplicationViewModel>? Applications);


public enum GetApplicationsStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2
}