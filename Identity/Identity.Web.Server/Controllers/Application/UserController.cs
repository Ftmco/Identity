using Identity.ViewModels.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("GetUserByToken")]
    public async Task<IActionResult> GetUserByToken(ApiRequest token)
    {
        var model = token.ReadRequestDataAsync<GetUserByToken>(HttpContext);
        return Ok();
    }

    [HttpPost("GetUserByUserName")]
    public async Task<IActionResult> GetUserByUserName(ApiRequest userName)
    {
        var model = userName.ReadRequestDataAsync<GetUserByUserName>(HttpContext);
        return Ok();
    }
}
