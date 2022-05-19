using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IPageGet : IAsyncDisposable
{
    Task<Page?> GetPageByNameAsync(string name, Guid appId, bool actvie);

    Task<IEnumerable<PagesRoles>> GetPageRolesAsync(Guid pageId);
}
