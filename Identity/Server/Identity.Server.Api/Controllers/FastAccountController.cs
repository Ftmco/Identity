using Identity.DataBase.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FastAccountController : ControllerBase
{
    readonly IFastAccountAction _fastAccount;

    public FastAccountController(IFastAccountAction fastAccount)
    {
        _fastAccount = fastAccount;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(FastLogin login)
    {
        return await _fastAccount.FastLoginAsync(login) switch
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
        var login = await _fastAccount.ActivationAsync(activation);
        return login.Status switch
        {
            LoginStatus.Success => Ok(Success("ورود موفق بود", "", new { login.Session })),
            LoginStatus.UserNotFound => Ok(Faild(404,"کاربری با این مشخصات یافت نشد","")),
            LoginStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }
}
