namespace Identity.Web.Server.Controllers.Application;

[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationRules _application;

    public ApplicationController(IApplicationRules application)
    {
        _application = application;
    }

    [HttpGet("GetApplications")]
    public async Task<IActionResult> GetApplications(int page, int count)
    {
        _application.GetApplcationsAsync(page, count, HttpContext);
        return Ok();
    }
}

