using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Identity.Authentication.Filter;

public class Auth : IAsyncActionFilter
{
    public string Role { get; set; } = "Any";

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        throw new NotImplementedException();
    }
}