using Identity.ViewModels.Application;

namespace Identity.ViewModels.Account;

public record SignUpViewModel(string UserName, string FullName, string Password, string? Email, string? MobileNo, ApplicationRequest? Application);

public record LoginViewModel(string UserName, string Password, ApplicationRequest? Application);

public record ChangePasswordViewModel(string CurrentPassword, string NewPassword);

public record UserViewModel(Guid Id, string UserName, string FullName, string Email, string MobileNo, DateTime RegisterDate);

public record ForgotPasswordViewModel(string UserName);

public record ResetPasswordViewModel(string UserName, string Code, string Password);