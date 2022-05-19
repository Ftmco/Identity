using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Implemention;

public class PageGet : IPageGet
{
    readonly IBaseQuery<Page, IdentityContext> _pageQuery;

    readonly IBaseQuery<PagesRoles, IdentityContext> _pagesRolesQuery;

    public PageGet(IBaseQuery<Page, IdentityContext> pageQuery, IBaseQuery<PagesRoles, IdentityContext> pagesRolesQuery)
    {
        _pageQuery = pageQuery;
        _pagesRolesQuery = pagesRolesQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Page?> GetPageByNameAsync(string name, Guid appId, bool active)
        => await _pageQuery.GetAsync(p => p.Name == name && p.ApplicationId == appId && p.IsActive == active);

    public async Task<IEnumerable<PagesRoles>> GetPageRolesAsync(Guid pageId)
        => await _pagesRolesQuery.GetAllAsync(pr => pr.PageId == pageId);
}
