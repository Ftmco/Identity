namespace Identity.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
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
