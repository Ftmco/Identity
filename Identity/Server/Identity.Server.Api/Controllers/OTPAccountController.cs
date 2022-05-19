using Identity.DataBase.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OTPAccountController : ControllerBase
{
    readonly IOtpAccountAction _otpAccount;

    public OTPAccountController(IOtpAccountAction otpAccount)
    {
        _otpAccount = otpAccount;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(OtpLogin login)
    {
        return await _otpAccount.OtpLoginAsync(login, Request.Headers) switch
        {
            LoginStatus.Success => Ok(Success("رمز یک بار مصرف برای شما ارسال شد", "", new { })),
            LoginStatus.UserNotFound => throw new NotImplementedException(),
            LoginStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }

    [HttpPost("Activation")]
    public async Task<IActionResult> ActivationAsync(Activation activation)
    {
        var login = await _otpAccount.ActivationAsync(activation, Request.Headers);
        return login.Status switch
        {
            LoginStatus.Success => Ok(Success("ورود موفق بود", "", new { login.Session })),
            LoginStatus.UserNotFound => Ok(Faild(404, "کاربری با این مشخصات یافت نشد", "")),
            LoginStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }
}
