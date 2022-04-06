namespace Identity.DataBase.ViewModel.Account;

public record SignUp(string FullName, string MobileNo, string Password);

public enum SignUpStatus
{
    Success = 0,
    UserExist = 1,
    Exception = 2
}