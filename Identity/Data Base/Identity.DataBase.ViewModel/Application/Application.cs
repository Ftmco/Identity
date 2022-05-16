using Newtonsoft.Json;

namespace Identity.DataBase.ViewModel;

public record ApplicationViewModel(string Key, string ApiKey)
{
    public static ApplicationViewModel? CreateApplication(string json) =>
    JsonConvert.DeserializeObject<ApplicationViewModel>(json);
}