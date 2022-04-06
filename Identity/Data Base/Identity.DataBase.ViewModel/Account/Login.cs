namespace Identity.DataBase.ViewModel.Account;

public record Login(string UserName,string Password);

public record LoginResponse(LoginStatus Status,Session Session);

public enum LoginStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2
}

public record Session(string Key,string Value);