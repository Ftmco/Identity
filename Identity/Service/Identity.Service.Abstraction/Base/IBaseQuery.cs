using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Service.Abstraction.Base;

public interface IBaseQuery<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);

    Task<TEntity?> GetAsync(object? id);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault);

    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<TEntity, bool>> count);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any);
}