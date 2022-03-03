using Identity.ViewModels.Api;
using Identity.ViewModels.Application;

namespace Identity.Web.Server.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileRules _profile;

    public ProfileController(IProfileRules profile)
    {
        _profile = profile;
    }

    [HttpPost("GetProfile")]
    public async Task<IActionResult> GetProfile(ApiRequest request)
    {
        var application = await request.ReadRequestDataAsync<ApplicationRequest>(HttpContext);
        ProfileResponse? profile = await _profile.GetProfileAsync(application, HttpContext);
        return profile.Status switch
        {
            GetprofileStatus.Success => Ok(await Success("Profile", "", profile.Profile).SendResponseAsync(HttpContext)),
            GetprofileStatus.ApplicationNotFoud => Ok(await Faild(404,"Application NotFound", "").SendResponseAsync(HttpContext)),
            GetprofileStatus.Exception => Ok(await ApiException("Exception", "Please Try Again").SendResponseAsync(HttpContext)),
            GetprofileStatus.UserNotFound => Ok(await Faild(404, "User Notfound", "Please Login to see yout profile").SendResponseAsync(HttpContext)),
            GetprofileStatus.EmptyProfile => Ok(await Faild(403, "Your Profile is empty for this application", "").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Exception", "Please Try Again").SendResponseAsync(HttpContext)),
        };
    }

    [HttpPost("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(ApiRequest request)
    {
        var profile = await request.ReadRequestDataAsync<UpdateProfileViewModel>(HttpContext);
        ProfileResponse? updateProfile = await _profile.UpdateProfileAsync(profile, HttpContext);
        return updateProfile.Status switch
        {
            GetprofileStatus.Success => Ok(await Success("Profile", "", updateProfile.Profile).SendResponseAsync(HttpContext)),
            GetprofileStatus.ApplicationNotFoud => Ok(await Faild(404, "Application NotFound", "").SendResponseAsync(HttpContext)),
            GetprofileStatus.Exception => Ok(await ApiException("Exception", "Please Try Again").SendResponseAsync(HttpContext)),
            GetprofileStatus.UserNotFound => Ok(await Faild(404, "User Notfound", "Please Login to see yout profile").SendResponseAsync(HttpContext)),
            GetprofileStatus.EmptyProfile => Ok(await Faild(403, "Your Profile is empty for this application", "").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Exception", "Please Try Again").SendResponseAsync(HttpContext)),
        };
    }
}

