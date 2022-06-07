using Identity.DataBase.ViewModel;
using Identity.Service.Tooling.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Implemention;

public class ApplicationViewModelService : IApplicationViewModel
{
    public Task<ApplicationInfo> CreateAppInfoViewModelAsync(Application application)
    {
        ApplicationInfo appInfo = new(Id: application.Id,
            ParentId: application.ApplicationId,
            Name: application.Name,
            ApiKey: application.ApiKey,
            Key: application.Key,
            CreateDate: application.CreateDate.ToShamsi(),
            IsActive: application.IsActive);
        return Task.FromResult(appInfo);
    }

    public Task<IEnumerable<ApplicationInfo>> CreateAppInfoViewModelAsync(IEnumerable<Application> applications)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
