
namespace Servant.Service.Implemention.Base;

public class BaseCud<TEntity, TContext> : IAsyncDisposable, IBaseCud<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    readonly TContext _context;

    readonly DbSet<TEntity> _dbSet;

    public BaseCud(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbSet.RemoveRange(entities);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(TEntity entity)
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
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        => await DeleteAsync(await _dbSet.Where(where).ToListAsync());

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> InsertAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SaveAsync()
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
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            _dbSet.UpdateRange(entities);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }
}