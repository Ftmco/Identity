namespace Servant.Service.Abstraction.Base;

public interface IBaseCud<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    Task<bool> InsertAsync(TEntity entity);

    Task<bool> InsertAsync(IEnumerable<TEntity> entities);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(TEntity entity);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

    Task<bool> SaveAsync();
}