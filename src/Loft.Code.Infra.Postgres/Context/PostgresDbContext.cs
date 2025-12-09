using System.Data;
using System.Linq.Expressions;
using Loft.Code.Domain.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loft.Code.Infra.Postgres.Context;

public class PostgresDbContext : DbContext, IAppDbContext
{

    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);

    }
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Database.BeginTransactionAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel)
    {
        return await Database.BeginTransactionAsync();
    }

    public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        await Set<TEntity>().AddRangeAsync(entities);
    }

    public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
    {
        return Set<TEntity>().Where(filter);
    }

    public IQueryable<TEntity> WhereForUpdate<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
    {
        var query = Set<TEntity>().Where(filter).ToQueryString() + " FOR UPDATE";
        return Set<TEntity>().FromSqlRaw(query);
    }

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().UpdateRange(entities);
    }
    public IQueryable<TEntity> FromSqlRaw<TEntity>(string fromSqlRaw) where TEntity : class
    {
        return Set<TEntity>().FromSqlRaw(fromSqlRaw);
    }
    public async Task<int> ExecuteDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await Set<TEntity>().Where(predicate).ExecuteDeleteAsync();
    }
    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().RemoveRange(entities);
    }
    public void DetachLocal<T>(T entity) where T : class
    {
        var local = Set<T>().Local.FirstOrDefault(entry => entry == entity);
        if (local != null)
        {
            Entry(local).State = EntityState.Detached;
        }
    }

    public async Task<List<TEntity>> SqlQueryRawAsync<TEntity>(string sql, params object[] parameters) where TEntity : class
    {
        return await Database.SqlQueryRaw<TEntity>(sql, parameters).ToListAsync();

    }
}