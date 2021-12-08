namespace Identity.Services.Base;

internal interface IBaseRules<Tentity> where Tentity : class
{
    Task<IEnumerable<Tentity>> GetAsync();

    Task<IEnumerable<Tentity>> GetAsync(Expression<Func<Tentity, bool>> where);

    Task<Tentity> GetAsync(object id);

    Task<bool> InsertAsync(Tentity entity);

    Task<bool> InsertAsync(IEnumerable<Tentity> tentities);

    Task<bool> UpdateAsync(Tentity entity);

    Task<bool> UpdateAsync(IEnumerable<Tentity> tentities);

    Task<bool> DeleteAsync(Tentity entity);

    Task<bool> DeleteAsync(IEnumerable<Tentity> tentities);

    Task<bool> DeleteAsync(object id);

    Task<bool> DeleteAsync(Expression<Func<Tentity, bool>> where);

    Task<bool> SaveAsync();
}

