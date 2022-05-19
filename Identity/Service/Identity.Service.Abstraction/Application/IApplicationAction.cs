using Grpc.Core;
using Identity.DataBase.ViewModel;

namespace Identity.Service.Abstraction;

public interface IApplicationAction : IAsyncDisposable
{
    /// <summary>
    /// Check and insert default role for user in login action
    /// </summary>
    /// <param name="userId">current user primary id</param>
    /// <param name="appId">currnet application primary id</param>
    Task CheckUserDefaultRoleAsync(Guid userId, Guid appId);

    /// <summary>
    /// Check user in application if is not in application insert it 
    /// </summary>
    /// <param name="appId">currnet application primary id</param>
    /// <param name="appId">currnet application primary id</param>
    /// <returns>Application user instance</returns>
    Task<ApplicationsUsers> CheckApplicationUserAsync(Guid appId, Guid userId);

    Task<UpsertApplicationResponse> UpsertAsync(UpsertApplication upsert, IHeaderDictionary headers);

    Task<bool> CheckUserAccessAsync(IHeaderDictionary header, CheckAccess checkAccess);

    Task<bool> CheckUserAccessAsync(Metadata header, CheckAccess checkAccess);
}
