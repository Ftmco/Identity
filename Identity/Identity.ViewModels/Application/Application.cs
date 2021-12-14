using Identity.ViewModels.File;

namespace Identity.ViewModels.Application;

public record ApplicationViewModel(Guid Id, string Name, string Image, string ApiKey);

public record ApplicationRequest(string ApiKey, string Password);

public record CUApplicationViewModel(Guid? Id, string Name, string Password, FileBase64ViewModel? File);

public record DeleteApplicationViewModel(Guid Id);