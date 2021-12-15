namespace Identity.Service.Rules;

public interface IProfileRules
{
    Task<ProfileResponse> GetProfileAsync(ApplicationRequest application, HttpContext httpContext);

    Task<ProfileResponse> UpdateProfileAsync(UpdateProfileViewModel profile,HttpContext httpContext);
}

