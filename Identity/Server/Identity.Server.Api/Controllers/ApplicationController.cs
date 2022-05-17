using Identity.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    readonly IApplicationAction _applicationAction;

    public ApplicationController(IApplicationAction applicationAction)
    {
        _applicationAction = applicationAction;
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertApplication upsert)
    {
        var app = await _applicationAction.UpsertAsync(upsert, Request.Headers);
        return app.Status switch
        {
            ApplicationActionStatus.Success => Ok(Success("اپلیکیشن با موفقیت ثبت شد", "", app.Application)),
            ApplicationActionStatus.UserNotFound => Ok(Faild(403, "برای دسترسی به این بخش وارد حساب خود شوید", "")),
            ApplicationActionStatus.Exception => Ok(ApiException()),
            ApplicationActionStatus.ApplicationNotFound => Ok(Faild(404, "اپلیکیشن مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
