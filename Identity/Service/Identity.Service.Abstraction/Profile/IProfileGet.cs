using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IProfileGet : IAsyncDisposable
{
    Task<ProfileResponse> GetProfileAsync(HttpContext httpContext);

    Task<ProfileResponse> GetProfileAsync(Guid userId);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsAsync(HttpContext httpContext);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsAsync(Guid? userId);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsWithBase64Async(Guid? userId);
}
