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
        var profile = await _profile.GetProfileAsync(application,HttpContext);
    }

    [HttpPost("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile()
    {
        return Ok();
    }
}

