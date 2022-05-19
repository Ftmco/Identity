using Grpc.Core;

namespace Identity.Server.Grpc.Services;

public class AuthService : Auth.AuthBase
{
    readonly IApplicationAction _applicationAction;

    public AuthService(IApplicationAction applicationAction)
    {
        _applicationAction = applicationAction;
    }

    public override async Task<CheckAccessReply> CheckAccess(CheckAccessRequest request, ServerCallContext context)
    {
        bool hasAccess = await _applicationAction.CheckUserAccessAsync(context.RequestHeaders,
            new(UserSession: request.Session, Address: request.Address, PageName: request.PageName));

        return new CheckAccessReply { HasAccess = hasAccess };
    }
}
