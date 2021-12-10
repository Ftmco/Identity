using Identity.ViewModels.Application;

namespace Identity.ViewModels.Account;

public record SignUpViewModel(string UserName, string FullName, string Password, string? Email, string? MobileNo, ApplicationRequest? Application);

public record LoginViewModel(string UserName, string Password, ApplicationRequest? Application);

public record ChangePasswordViewModel(string CurrentPassword, string NewPassword, ApplicationRequest? Application);