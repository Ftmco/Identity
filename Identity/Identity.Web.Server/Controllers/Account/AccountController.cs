using Identity.Tools.Api;
using Identity.ViewModels.Api;

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
    public async Task<IActionResult> Login(ApiRequest request)
    {
        LoginViewModel? login = await request.ReadRequestDataAsync<LoginViewModel>(HttpContext);
        LoginResponse loginResult = await _account.LoginAsync(login);
        return loginResult.Status switch
        {
            LoginStatus.Success => Ok(await Success("Success", "Login Successfully", new { loginResult.Session?.Key, loginResult.Session?.Value }).SendResponseAsync(HttpContext)),
            LoginStatus.UserNotFound => Ok(await Faild(404, "User Not Found", "Wrong UserName or Password").SendResponseAsync(HttpContext)),
            LoginStatus.Exception => Ok(await ApiException("Exception", "Please Try Again To Login").SendResponseAsync(HttpContext)),
            LoginStatus.ApplicationNotFound => Ok(await Faild(404, "application not found", "").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Exception", "Please Try Again To Login").SendResponseAsync(HttpContext)),
        };
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(ApiRequest request)
    {
        var signUp = await request.ReadRequestDataAsync<SignUpViewModel>(HttpContext);
        SignUpResponse signUpUser = await _account.SignUpAsync(signUp);
        return signUpUser.Status switch
        {
            SignUpStatus.Success => Ok(await Success("Successfully To Create Account", "", new { signUpUser.User }).SendResponseAsync(HttpContext)),
            SignUpStatus.UserExist => Ok(await Faild(403, "User Exist", "").SendResponseAsync(HttpContext)),
            SignUpStatus.Exception => Ok(await ApiException("Exception When Create Account Please Try Agian", "").SendResponseAsync(HttpContext)),
            SignUpStatus.ApplicationNotFound => Ok(await Faild(404, "Application Notfound", "").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Exception When Create Account Please Try Agian", "").SendResponseAsync(HttpContext)),
        };
    }

    [HttpPost("LogOut")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ApiRequest request)
    {
        ChangePasswordViewModel? changePassword = await request.ReadRequestDataAsync<ChangePasswordViewModel>(HttpContext);
        ChangePasswordStatus change = await _account.ChangePasswordAsync(changePassword, HttpContext);
        return change switch
        {
            ChangePasswordStatus.Success => Ok(await Success("Succes To Change Password", "", new { }).SendResponseAsync(HttpContext)),
            ChangePasswordStatus.WrongPassword => Ok(await Faild(403, "Wrong Password", "").SendResponseAsync(HttpContext)),
            ChangePasswordStatus.Exception => Ok(await ApiException("Exception To Change Password Please Try Again", "").SendResponseAsync(HttpContext)),
            ChangePasswordStatus.UserNotFound => Ok(await Faild(404, "User Notfoun", "Please Login To Change Password").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Exception To Change Password Please Try Again", "").SendResponseAsync(HttpContext)),
        };
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(ApiRequest request)
    {
        ForgotPasswordViewModel? forgotPassword = await request.ReadRequestDataAsync<ForgotPasswordViewModel>(HttpContext);
        return await _account.ForgotPasswordAsync(forgotPassword) switch
        {
            ForgotPasswordStatus.Success => throw new NotImplementedException(),
            ForgotPasswordStatus.UserNotFound => throw new NotImplementedException(),
            ForgotPasswordStatus.Exception => throw new NotImplementedException(),
            ForgotPasswordStatus.WrongCode => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ApiRequest request)
    {
        ResetPasswordViewModel? resetPassword = await request.ReadRequestDataAsync<ResetPasswordViewModel>(HttpContext);
        return await _account.ResetPasswordAsync(resetPassword) switch
        {
            ForgotPasswordStatus.Success => throw new NotImplementedException(),
            ForgotPasswordStatus.UserNotFound => throw new NotImplementedException(),
            ForgotPasswordStatus.Exception => throw new NotImplementedException(),
            ForgotPasswordStatus.WrongCode => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
