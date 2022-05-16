using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IProfileAction : IAsyncDisposable
{
    Task<ProfileResponse> UpdateProfileAsync(UpdateProfile updateProfile, HttpContext httpContext);
}
