using Identity.DataBase.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    readonly IProfileGet _profileGet;

    readonly IProfileAction _profileAction;

    public ProfileController(IProfileGet profileGet, IProfileAction profileAction)
    {
        _profileGet = profileGet;
        _profileAction = profileAction;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetProfileAsync(Guid? userId)
    {
        ProfileResponse? profile = userId == null
            ? await _profileGet.GetProfileAsync(HttpContext)
                : await _profileGet.GetProfileAsync(userId.Value);

        return profile.Status switch
        {
            ProfileStatus.Success => Ok(Success("", "", profile.Profile)),
            ProfileStatus.NotFound => Ok(Faild(404, "پروفایل خود را تکمیل کنید", "")),
            ProfileStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            ProfileStatus.NotAuthorized => Ok(Faild(403, "برای مشاهده پروفایل وارد حساب خود شوید", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfile update)
    {
        var profile = await _profileAction.UpdateProfileAsync(update, HttpContext);
        return profile.Status switch
        {
            ProfileStatus.Success => Ok(Success("مشخصات کاربر با موفقیت ویرایش شد", "", profile.Profile)),
            ProfileStatus.NotFound => Ok(Faild(404, "پروفایل خود را تکمیل کنید", "")),
            ProfileStatus.Exception => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
            ProfileStatus.NotAuthorized => Ok(Faild(403, "برای مشاهده پروفایل وارد حساب خود شوید", "")),
            _ => Ok(ApiException("خطایی رخ داد مجددا تلاش کنید", "")),
        };
    }
}
