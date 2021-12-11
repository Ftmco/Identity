namespace Identity.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRules _account;

    public AccountController(IAccountRules account)
    {
        _account = account;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        LoginResponse loginResult = await _account.LoginAsync(login);
        return loginResult.Status switch
        {
            LoginStatus.Success => Ok(Success("Success", "Login Successfully", new { loginResult.Session.Key, loginResult.Session.Value })),
            LoginStatus.UserNotFound => Ok(Notfound("User Not Found", "Wrong UserName or Password")),
            LoginStatus.Exception => Ok(Excetpion("Exception", "Please Try Again To Login")),
            _ => Ok(Excetpion("Exception", "Please Try Again To Login")),
        };
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpViewModel signUp)
    {
        SignUpResponse signUpUser = await _account.SignUpAsync(signUp);
        return signUpUser.Status switch
        {
            SignUpStatus.Success => Ok(Success("Successfully To Create Account", "", new { signUpUser.User })),
            SignUpStatus.UserExist => Ok(AccessDenied("User Exist", "")),
            SignUpStatus.Exception => Ok(Excetpion("Exception When Create Account Please Try Agian", "")),
            SignUpStatus.ApplicationNotFound => Ok(Notfound("Application Notfound", "")),
            _ => Ok(Excetpion("Exception When Create Account Please Try Agian", "")),
        };
    }

    [HttpPost("LogOut")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
    {
        ChangePasswordStatus change = await _account.ChangePasswordAsync(changePassword, HttpContext);
        return change switch
        {
            ChangePasswordStatus.Success => Ok(Success("Succes To Change Password", "", new { })),
            ChangePasswordStatus.WrongPassword => Ok(AccessDenied("Wrong Password", "")),
            ChangePasswordStatus.Exception => Ok(Excetpion("Exception To Change Password Please Try Again", "")),
            ChangePasswordStatus.UserNotFound => Ok(Notfound("User Notfoun", "Please Login To Change Password")),
            _ => Ok(Excetpion("Exception To Change Password Please Try Again", "")),
        };
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }
}
