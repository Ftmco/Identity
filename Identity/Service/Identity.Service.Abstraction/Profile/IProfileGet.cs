using Identity.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IProfileGet : IAsyncDisposable
{
    Task<ProfileResponse> GetProfileAsync(HttpContext httpContext);

    Task<ProfileResponse> GetProfileAsync(Guid userId);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsAsync(HttpContext httpContext);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsAsync(Guid? userId);

    Task<IEnumerable<FileViewModel>> GetUserAvatarsWithBase64Async(Guid? userId);
}
