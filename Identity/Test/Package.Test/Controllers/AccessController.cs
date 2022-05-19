using Identity.Authentication.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Package.Test.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessController : ControllerBase
{
    readonly IConfiguration _configuration;

    public AccessController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //[Auth(PageName = "Owner")]
    [HttpGet("CheckAccess")]
    public async Task<IActionResult> CheckAccess()
    {
        return await HttpContext.CheckAccessAsync("AdminHome", _configuration)
            ? Ok(new { Access = true }) : Ok(new { Access = false });
    }
}
