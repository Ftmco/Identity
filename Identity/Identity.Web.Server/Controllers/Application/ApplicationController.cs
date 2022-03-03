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
            ActionApplicationsStatus.Success => Ok(Success("Applications", "", new { applications.Applications })),
            ActionApplicationsStatus.UserNotFound => Ok(Faild(404, "User Notfound", "Please Login to See your applications")),
            ActionApplicationsStatus.Exception => Ok(ApiException("Exception", "Please Try again to get applications")),
            _ => Ok(ApiException("Exception", "Please Try again to get applications")),
        };
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CUApplicationViewModel create)
    {
        CUApplicationResponse? createApplication = await _application.CreateApplicationAsync(create, HttpContext);

        return createApplication.Status switch
        {
            CUApplicationStatus.Success => Ok(Success($"Success To Create Application : {create.Name}", "", new { createApplication.Application })),
            CUApplicationStatus.UserNotFound => Ok(Faild(404, "User Notfound", "Please Login to See your applications")),
            CUApplicationStatus.Exception => Ok(ApiException("Exception", "Please Try again to create application")),
            CUApplicationStatus.ApplicationNotFound => Ok(Faild(404, "Application Notfound", "Application is not exist")),
            CUApplicationStatus.ApplicationExist => Ok(Faild(403, "Application Has Exist", $"An application has exist with this name : {create.Name}")),
            _ => Ok(ApiException("Exception", "Please Try again to create application")),
        };
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(CUApplicationViewModel update)
    {
        CUApplicationResponse? updateApplicatin = await _application.UpdateApplicationAsync(update, HttpContext);
        return updateApplicatin.Status switch
        {
            CUApplicationStatus.Success => Ok(Success($"Success To Update Application : {updateApplicatin.Application.Name}", "", new { updateApplicatin.Application })),
            CUApplicationStatus.UserNotFound => Ok(Faild(404, "User Notfound", "Please Login to update application")),
            CUApplicationStatus.Exception => Ok(ApiException("Exception", "Please Try again to update application")),
            CUApplicationStatus.ApplicationNotFound => Ok(Faild(404, "Application Notfound", "Application is not exist")),
            CUApplicationStatus.ApplicationExist => Ok(Faild(403, "Application Has Exist", $"An application has exist with this name : {update.Name}")),
            _ => Ok(ApiException("Exception", "Please Try again to update application")),
        };
    }


    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(DeleteApplicationViewModel dApp)
    {
        DeleteApplicationResponse? delete = await _application.DeleteApplicationAsync(dApp, HttpContext);
        return delete.Status switch
        {
            ActionApplicationsStatus.Success => Ok(Success($"Success To Delete Application : {delete.AppName}", "", new { delete.Id })),
            ActionApplicationsStatus.UserNotFound => Ok(Faild(404, "User Notfound", "Please Login to delete application")),
            ActionApplicationsStatus.Exception => Ok(ApiException("Exception", "Please Try again to delete application")),
            ActionApplicationsStatus.ApplicationNotFound => Ok(Faild(404, "Application Notfound", "Application is not exist")),
            _ => Ok(ApiException("Exception", "Please Try again to delete application")),
        };
    }

    [HttpPost("GetUsers")]
    public async Task<IActionResult> GetUsers(ApplicationRequest application)
    {
        GetApplicationUsersResponse? users = await _application.GetUsersAsync(application);
        return users.Status switch
        {
            GetApplicationUsersStatus.Success => Ok(Success("User Applications", "", new { users.Users })),
            GetApplicationUsersStatus.ApplicationNotFound => Ok(Faild(404, "Application Notfound", "")),
            GetApplicationUsersStatus.Exception => Ok(ApiException("Exception", "Please Try again to get application users")),
            _ => Ok(ApiException("Exception", "Please Try again to get application users")),
        };
    }
}

