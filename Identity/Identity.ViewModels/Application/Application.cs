namespace Identity.ViewModels.Application;

public record ApplicationViewModel(Guid Id,string Name, string Image, string ApiKey);

public record ApplicationRequest(string ApiKey,string Password);