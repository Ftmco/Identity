namespace Identity.Web.Server.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{

    [HttpGet("GetProfile")]
    public async Task<IActionResult> GetProfile()
    {
        return Ok();
    }

    [HttpPost("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile()
    {
        return Ok();
    }
}

