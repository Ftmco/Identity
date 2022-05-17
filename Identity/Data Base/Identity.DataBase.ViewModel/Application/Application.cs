using Newtonsoft.Json;

namespace Identity.DataBase.ViewModel;

public record ApplicationViewModel(string Key, string ApiKey)
{
    public static ApplicationViewModel? CreateApplication(string json) =>
    JsonConvert.DeserializeObject<ApplicationViewModel>(json);
}

public record UpsertApplication(Guid? Id, string Name);

public record ApplicationInfo(Guid Id, string Name, string ApiKey, string Key, DateTime CreateDate, bool IsActive);

public record UpsertApplicationResponse(ApplicationActionStatus Status, ApplicationInfo? Application);

public enum ApplicationActionStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    ApplicationNotFound = 3
}