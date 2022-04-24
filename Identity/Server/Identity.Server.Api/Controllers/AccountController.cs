using Identity.DataBase.ViewModel.Account;
using Identity.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    readonly IAccountAction _account;

    public AccountController(IAccountAction account)
    {
        _account = account;
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> SignUpAsync(SignUp signUp)
        => await _account.SignUpAsync(signUp) switch
        {
            SignUpStatus.Success => Ok(Success("کاربر با موفقیت ثبت شد", "", new { })),
            SignUpStatus.UserExist => Ok(Faild(403, "کاربری با این مشخاصت موجود است", "")),
            SignUpStatus.Exception => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
            _ => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
        };

    [HttpPost("Activation")]
    public async Task<IActionResult> ActivationAsync(Activation activation)
        => await _account.ActivationAsync(activation) switch
        {
            ActivationStatus.Success => Ok(Success("حساب کاربری با موفقیت فعال شد", "", new { })),
            ActivationStatus.UserNotFound => Ok(Faild(404, "کاربری با این مشخصات یافت نشد", "")),
            ActivationStatus.Exception => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
            ActivationStatus.WrongActiveCode => Ok(Faild(403, "کد فعال سازی اشتباه است", "")),
            _ => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
        };

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(Login login)
    {
        var loginUser = await _account.LoginAsync(login);
        return loginUser.Status switch
        {
            LoginStatus.Success => Ok(Success("ورود موفق بود", "", loginUser.Session)),
            LoginStatus.UserNotFound => Ok(Faild(404, "کاربری با این مشخصات یافت نشد", "")),
            LoginStatus.Exception => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
            _ => Ok(ApiException("حطایی رخ داد مجددا تلاش کنید", "")),
        };
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _account.LogoutAsync(HttpContext);
        return Ok(Success("کاربر با موفقیت از حساب خارج شد", "", new { }));
    }
}
