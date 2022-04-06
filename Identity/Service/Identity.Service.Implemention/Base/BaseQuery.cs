namespace Servant.Service.Implemention.Base;

public class BaseQuery<TEntity, TContext> : IAsyncDisposable, IBaseQuery<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    readonly TContext _context;

    readonly DbSet<TEntity> _dbSet;

    public BaseQuery(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any)
        => await _dbSet.AnyAsync(any);

    public async Task<int> CountAsync()
        => await _dbSet.CountAsync();

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> count)
        => await _dbSet.CountAsync(count);

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        => await _dbSet.Where(where).ToListAsync();

    public async Task<TEntity?> GetAsync(object? id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault)
        => await _dbSet.FirstOrDefaultAsync(firstOrDefault);
}