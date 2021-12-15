namespace Identity.Service.Rules;

public interface IProfileRules
{
    Task<GetProfileResponse> GetProfileAsync(ApplicationRequest application, HttpContext httpContext);

    Task UpdateProfileAsync();
}

