using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loft.Code.Domain.Interfaces.DbContext;

public interface IAppDbContext
{
    Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync();
    EntityEntry<T> Entry<T>(T entity) where T : class;
    EntityEntry<T> Attach<T>(T entity) where T : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
    Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
    Task<int> ExecuteDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void DetachLocal<T>(T entity) where T : class;
    IQueryable<TEntity> FromSqlRaw<TEntity>(string fromSqlRaw) where TEntity : class;
    Task<List<TEntity>> SqlQueryRawAsync<TEntity>(string sql, params object[] parameters) where TEntity : class;

    IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
    IQueryable<TEntity> WhereForUpdate<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

}
