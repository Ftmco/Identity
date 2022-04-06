namespace Identity.DataBase.ViewModel.Account;

public record Activation(string UserName, string ActiveCode);

public enum ActivationStatus
{
    Success = 0,
    UserNotFound = 1,
    Exception = 2,
    WrongActiveCode = 3,
}