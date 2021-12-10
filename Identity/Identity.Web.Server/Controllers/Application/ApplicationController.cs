using Identity.ViewModels.Application;

namespace Identity.Web.Server.Controllers;

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
        GetApplicationsResponse? applications = await _application.GetApplcationsAsync(page, count, HttpContext);
        return applications.Status switch
        {
            GetApplicationsStatus.Success => Ok(Success("Applications", "", new { applications.Applications })),
            GetApplicationsStatus.UserNotFound => Ok(Notfound("User Notfound", "Please Login to See your applications")),
            GetApplicationsStatus.Exception => Ok(Excetpion("Exception", "Please Try again to get applications")),
            _ => Ok(Excetpion("Exception", "Please Try again to get applications")),
        };
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CUApplicationViewModel create)
    {
        CUApplicationResponse? createApplication = await _application.CreateApplicationAsync(create, HttpContext);

        return createApplication.Status switch
        {
            CUApplicationStatus.Success => Ok(Success($"Success To Create Application : {create.Name}", "", new { createApplication.Application })),
            CUApplicationStatus.UserNotFound => Ok(Notfound("User Notfound", "Please Login to See your applications")),
            CUApplicationStatus.Exception => Ok(Excetpion("Exception", "Please Try again to create applications")),
            CUApplicationStatus.ApplicationNotFound => Ok(Notfound("Application Notfound", "Application is not exist")),
            CUApplicationStatus.ApplicationExist => Ok(AccessDenied("Application Has Exist", $"An application has exist with this name : {create.Name}")),
            _ => Ok(Excetpion("Exception", "Please Try again to create applications")),
        };
    }
}

