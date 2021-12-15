using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Services;

public class ProfileService : IProfileRules, IDisposable
{

    private readonly IApplicationRules _application;

    private readonly IAccountRules _account;

    private readonly IBaseRules<Profile> _profileCrud;

    public void Dispose()
    {
       GC.SuppressFinalize(this);
    }

    public async Task GetProfileAsync(ApplicationRequest application,HttpContext httpContext)
        => await Task.Run(async () =>
        {
            Application findApplication = await _application.FindApplicationAsync(application);
            if (findApplication != null)
            {
                User user = await _account.GetUserAsync(httpContext);
                if(user != null)
                {
                    Profile profile = await _profileCrud.GetOneAsync(p => p.UserId == user.Id && p.ApplicationId == findApplication.Id);
                }
            }
            
        });

    public Task UpdateProfileAsync()
    {
        throw new NotImplementedException();
    }
}