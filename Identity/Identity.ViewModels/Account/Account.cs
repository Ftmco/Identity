namespace Identity.ViewModels.Account;

public record SignUpViewModel(string UserName, string FullName, string Password, string? Email, string? MobileNo);

public record LoginViewModel(string UserName,string Password);

public record ChangePasswordViewModel(string CurrentPassword,string NewPassword);