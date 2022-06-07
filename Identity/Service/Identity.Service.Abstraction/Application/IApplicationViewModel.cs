using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IApplicationViewModel : IAsyncDisposable
{
    Task<ApplicationInfo> CreateAppInfoViewModelAsync(Application application);

    Task<IEnumerable<ApplicationInfo>> CreateAppInfoViewModelAsync(IEnumerable<Application> applications);
}