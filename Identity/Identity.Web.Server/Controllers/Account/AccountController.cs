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
            LoginStatus.Success => Ok(Success("Success", "Login Successfully", new { loginResult.Session })),
            LoginStatus.UserNotFound => Ok(Notfound("User Not Found", "Wrong UserName or Password")),
            LoginStatus.Exception => Ok(Excetpion("Exception", "Please Try Again To Login")),
            _ => Ok(Excetpion("Exception", "Please Try Again To Login")),
        };
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp()
    {
        return Ok();
    }

    [HttpPost("LogOut")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword()
    {
        return Ok();
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
