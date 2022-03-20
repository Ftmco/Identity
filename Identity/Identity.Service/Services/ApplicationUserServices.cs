using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Services;

public class ApplicationUserServices : IApplicationUserRules
{
    readonly IApplicationRules _application;

    readonly ISessionRules _session;

    readonly IAccountRules _account;

    public ApplicationUserServices(IApplicationRules application, ISessionRules session, IAccountRules account)
    {
        _application = application;
        _session = session;
        _account = account;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<GetUserResponse> GetUserByTokenAsync(GetUserByToken getUserByToken)
    {
        Application application = await _application.FindApplicationAsync(getUserByToken.Application);
        if (application != null)
        {
            Session session = await _session.GetSessionAsync(getUserByToken.Token);
            if (session != null && await _application.CheckUserInAppliactionAsync(session, application))
            {
                User user = await _account.GetUserFromAuthTokenAsync(session);
                if (user != null)
                {
                    UserViewModel userViewModel = await _account.CreateUserViewModelAsync(user);
                    return new GetUserResponse(GetUserStatus.Success, userViewModel);
                }
                return new GetUserResponse(GetUserStatus.UserNotFound, null);
            }
            return new GetUserResponse(GetUserStatus.UserNotFound, null);
        }
        return new GetUserResponse(GetUserStatus.ApplicationNotFound, null);
    }

    public Task<GetUserResponse> GetUserByUserNameAsync(GetUserByUserName getUserByUserName)
    {
        throw new NotImplementedException();
    }
}
