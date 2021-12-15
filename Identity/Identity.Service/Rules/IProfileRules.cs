namespace Identity.Service.Rules;

public interface IProfileRules
{
    Task GetProfileAsync(ApplicationRequest application,HttpContext httpContext);

    Task UpdateProfileAsync();
}

