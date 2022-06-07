using Newtonsoft.Json;

namespace Identity.DataBase.ViewModel;

public record ApplicationViewModel(string Key, string ApiKey)
{
    public static ApplicationViewModel? CreateApplication(string json) =>
    JsonConvert.DeserializeObject<ApplicationViewModel>(json);
}

public record UpsertApplication(Guid? Id, Guid? ParentId, string Name);

public record ApplicationInfo(Guid Id, Guid? ParentId, string Name, string ApiKey, string Key, string CreateDate, bool IsActive)
{
    public IEnumerable<ApplicationInfo>? Childs { get; set; }
}

public record GetApplicationResponse(ApplicationActionStatus Status, IEnumerable<ApplicationInfo> Applications);

public record UpsertApplicationResponse(ApplicationActionStatus Status, ApplicationInfo? Application);

public record CheckAccess(string? UserSession, string? Address, string PageName);

public enum ApplicationActionStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3,
    AccessDenied = 4
}