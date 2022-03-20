using Identity.ViewModels.Application;

namespace Identity.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    readonly IApplicationUserRules _applicationUsers;

    public UserController(IApplicationUserRules applicationUsers)
    {
        _applicationUsers = applicationUsers;
    }

    [HttpPost("GetUserByToken")]
    public async Task<IActionResult> GetUserByToken(ApiRequest token)
    {
        GetUserByToken? model = await token.ReadRequestDataAsync<GetUserByToken>(HttpContext);
        GetUserResponse? user = await _applicationUsers.GetUserByTokenAsync(model);
        return user.Status switch
        {
            GetUserStatus.Success => Ok(await Success("", "", new { user.User }).SendResponseAsync(HttpContext)),
            GetUserStatus.ApplicationNotFound => Ok(await Faild(404,"Application Not Found","Please Check Apikey and Password").SendResponseAsync(HttpContext)),
            GetUserStatus.Exception => Ok(await ApiException("Please Try Again","").SendResponseAsync(HttpContext)),
            GetUserStatus.UserNotFound => Ok(await Faild(404,"User Not Found,Check Token","").SendResponseAsync(HttpContext)),
            _ => Ok(await ApiException("Please Try Again", "").SendResponseAsync(HttpContext)),
        };
    }

    [HttpPost("GetUserByUserName")]
    public async Task<IActionResult> GetUserByUserName(ApiRequest userName)
    {
        var model = userName.ReadRequestDataAsync<GetUserByUserName>(HttpContext);
        return Ok();
    }
}
