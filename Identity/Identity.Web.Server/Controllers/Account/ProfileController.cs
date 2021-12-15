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
    public async Task<IActionResult> GetProfile(ApplicationRequest application)
    {
        ProfileResponse? profile = await _profile.GetProfileAsync(application, HttpContext);
        return profile.Status switch
        {
            GetprofileStatus.Success => Ok(Success("Profile", "", profile.Profile)),
            GetprofileStatus.ApplicationNotFoud => Ok(Notfound("Application NotFound", "")),
            GetprofileStatus.Exception => Ok(Excetpion("Exception", "Please Try Again")),
            GetprofileStatus.UserNotFound => Ok(Notfound("User Notfound", "Please Login to see yout profile")),
            GetprofileStatus.EmptyProfile => Ok(AccessDenied("Your Profile is empty for this application", "")),
            _ => Ok(Excetpion("Exception", "Please Try Again")),
        };
    }

    [HttpPost("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel profile)
    {
        ProfileResponse? updateProfile = await _profile.UpdateProfileAsync(profile, HttpContext);
        return updateProfile.Status switch
        {
            GetprofileStatus.Success => Ok(Success("Profile", "", updateProfile.Profile)),
            GetprofileStatus.ApplicationNotFoud => Ok(Notfound("Application NotFound", "")),
            GetprofileStatus.Exception => Ok(Excetpion("Exception", "Please Try Again")),
            GetprofileStatus.UserNotFound => Ok(Notfound("User Notfound", "Please Login to see yout profile")),
            GetprofileStatus.EmptyProfile => Ok(AccessDenied("Your Profile is empty for this application", "")),
            _ => Ok(Excetpion("Exception", "Please Try Again")),
        };
    }
}

