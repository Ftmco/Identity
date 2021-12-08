namespace Identity.Services.Base;

public class BaseServices<TEntity> : IDisposable, IBaseRules<TEntity> where TEntity : class
{
    #region -- Dependency --

    /// <summary>
    /// Data Base Context
    /// </summary>
    private readonly IdentityContext _context;

    /// <summary>
    /// Db Set , Current Table
    /// </summary>
    private readonly DbSet<TEntity> _dbSet;

    public BaseServices(IdentityContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    #endregion

    public async Task<bool> DeleteAsync(TEntity entity)
    => await Task.Run(async () =>
    {
        try
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    });

    public async Task<bool> DeleteAsync(IEnumerable<TEntity> tentities)
    => await Task.Run(async () =>
    {
        try
        {
            _dbSet.RemoveRange(tentities);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    });

    public async Task<bool> DeleteAsync(object id)
        => await Task.FromResult(await DeleteAsync(await GetAsync(id)));

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
    => await Task.FromResult(await DeleteAsync(await GetAsync(where)));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    => await Task.FromResult(await _dbSet.ToListAsync());

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where)
        => await Task.FromResult(await _dbSet.Where(where).ToListAsync());

    public async Task<TEntity> GetAsync(object id)
        => await Task.FromResult(await _dbSet.FindAsync(id));

    public async Task<bool> InsertAsync(TEntity entity)
        => await Task.Run(async () =>
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return await SaveAsync(); ;
            }
            catch
            {
                return false;
            }
        });

    public async Task<bool> InsertAsync(IEnumerable<TEntity> tentities)
    => await Task.Run(async () =>
     {
         try
         {
             await _dbSet.AddRangeAsync(tentities);
             return await SaveAsync(); ;
         }
         catch
         {
             return false;
         }
     });

    public async Task<bool> SaveAsync()
        => await Task.Run(async () =>
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        });

    public async Task<bool> UpdateAsync(TEntity entity)
        => await Task.Run(async () =>
        {
            try
            {
                _dbSet.Update(entity);
                return await SaveAsync(); ;
            }
            catch
            {
                return false;
            }
        });

    public async Task<bool> UpdateAsync(IEnumerable<TEntity> tentities)
     => await Task.Run(async () =>
     {
         try
         {
             _dbSet.UpdateRange(tentities);
             return await SaveAsync(); ;
         }
         catch
         {
             return false;
         }
     });
}

