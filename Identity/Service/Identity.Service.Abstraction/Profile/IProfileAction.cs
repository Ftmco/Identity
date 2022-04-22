using Identity.DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IProfileAction : IAsyncDisposable
{
    Task<ProfileResponse> UpdateProfileAsync(UpdateProfile updateProfile,HttpContext httpContext);
}
