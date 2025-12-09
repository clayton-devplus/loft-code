using System.Linq.Expressions;
using Loft.Code.Domain.Enums;
using Loft.Code.Domain.Interfaces;
using Loft.Code.Domain.Interfaces.DbContext;
using Loft.Code.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loft.Code.Domain.Repository;


public abstract class RepositoryBase<T, TKey> : IRepositoryBase<T, TKey> where T : class
{
    public readonly IAppDbContext _context;

    protected RepositoryBase(IDbContextFactory dbContextFactory, TypeDbEnum contextType)
    {
        _context = dbContextFactory.CreateDbContext(contextType);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Where(predicate).AnyAsync();
    }
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Where(predicate).CountAsync();
    }

    public async Task<bool> DeleteAsync(T entityToDelete)
    {
        if (entityToDelete is ISoftDelete softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            softDeleteEntity.DeletedAt = DateTime.Now;
            softDeleteEntity.DeletedBy = 0;

            _context.Update(entityToDelete);
        }
        else
        {
            _context.Remove(entityToDelete);
        }

        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> DeleteWithPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        var hasRecords = await _context.Where(predicate).AnyAsync();

        if (!hasRecords)
            return false; // ✅ Não há nada para deletar

        var affected = await _context.ExecuteDeleteAsync<T>(predicate);
        return affected > 0;
    }
    public async Task<bool> DeleteRangeAsync(IEnumerable<T> entitiesToDelete)
    {
        var entitiesToDeleteList = entitiesToDelete.ToList();

        var softDeleteEntities = entitiesToDeleteList.OfType<ISoftDelete>().ToList();
        if (softDeleteEntities.Any())
        {
            softDeleteEntities.ForEach(entity =>
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = 0;
            });
            _context.UpdateRange(softDeleteEntities);
        }

        var hardDeleteEntities = entitiesToDeleteList.Except(softDeleteEntities.Cast<T>()).ToList();
        if (hardDeleteEntities.Any())
        {
            _context.RemoveRange(hardDeleteEntities);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public Task<T?> GetByIdAsync(TKey id)
    {
        var parameter = Expression.Parameter(typeof(T), "p");
        var property = Expression.PropertyOrField(parameter, "Id");
        var idValue = Expression.Constant(id);
        var equals = Expression.Equal(property, idValue);
        var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

        return _context.Where(lambda).FirstOrDefaultAsync();
    }

    public async Task<T?> QueryFirsOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        var dbQuery = _context.Where(filter);

        foreach (var include in includes)
            dbQuery = dbQuery.Include(include);

        return await dbQuery.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> QueryFromSqlRawAsync(string fromSqlRaw)
    {
        return await _context.FromSqlRaw<T>(fromSqlRaw).ToListAsync();
    }
    // Adicione este método genérico

    public async Task<List<TEntity>> SqlQueryRawAsync<TEntity>(string sql, params object[] parameters) where TEntity : class
    {
        return await _context.SqlQueryRawAsync<TEntity>(sql, parameters);
    }
    public async Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        var dbQuery = _context.Where(filter);

        foreach (var include in includes)
            dbQuery = dbQuery.Include(include);

        return await dbQuery.ToListAsync();
    }

    public async Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> selectFields, params Expression<Func<T, object>>[] includes)
    {
        var dbQuery = _context.Where(filter);

        foreach (var include in includes)
            dbQuery = dbQuery.Include(include);

        if (selectFields != null)
            dbQuery = dbQuery.Select(selectFields);

        return await dbQuery.ToListAsync();
    }

    public async Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter,
                                                     Expression<Func<T, object>>? orderBy = null,
                                                     Expression<Func<T, T>>? selectFields = null,
                                                     params Expression<Func<T, object>>[] includes)
    {
        var dbQuery = _context.Where(filter);

        foreach (var include in includes)
            dbQuery = dbQuery.Include(include);

        if (selectFields != null)
            dbQuery = dbQuery.Select(selectFields);

        if (orderBy != null)
            dbQuery = dbQuery.OrderBy(orderBy);

        return await dbQuery.ToListAsync();
    }

    public async Task<(IEnumerable<T> models, bool hasNext)> QueryListAsync(Expression<Func<T, bool>> filter,
                                                                                        int page = 1,
                                                                                        int pageSize = 10,
                                                                                        string? search = "",
                                                                                        Expression<Func<T, object>>? orderBy = null,
                                                                                        bool orderByDescending = false,
                                                                                        params Expression<Func<T, object>>[] includes)
    {
        var dbQuery = _context.Where(filter);

        foreach (var include in includes)
        {
            dbQuery = dbQuery.Include(include);
        }

        if (orderBy != null)
        {
            if (orderByDescending)
                dbQuery = dbQuery.OrderByDescending(orderBy);
            else
                dbQuery = dbQuery.OrderBy(orderBy);
        }


        var listModels = await dbQuery.Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .AsNoTracking()
                                      .ToListAsync();

        return (listModels, listModels.Count == pageSize);
    }

    public async Task<bool> SaveAsync(T entity)
    {
        await _context.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> SaveRangeAsync(IEnumerable<T> entitiesToSave)
    {
        await _context.AddRangeAsync(entitiesToSave);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> SafeUpdateAsync(T entity)
    {
        return false;
        // var existingEntity = await GetByIdAsync(entity.Id);
        // if (existingEntity != null)
        // {
        //     _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        //     return await _context.SaveChangesAsync() > 0;
        // }
        // else
        // {
        //     _context.Attach(entity);
        //     _context.Entry(entity).State = EntityState.Modified;
        //     return await _context.SaveChangesAsync() > 0;
        // }

    }

    public async Task<IEnumerable<TResult>> QueryFromSqlRawAsync<TResult>(string fromSqlRaw, params object[] parameters) where TResult : class
    {
        return await _context.SqlQueryRawAsync<TResult>(fromSqlRaw, parameters);
    }
}
public abstract class RepositoryBase<T> : RepositoryBase<T, long>, IRepositoryBase<T> where T : class
{

    protected RepositoryBase(IDbContextFactory dbContextFactory) : base(dbContextFactory, TypeDbEnum.Application)
    {

    }
    protected RepositoryBase(IDbContextFactory dbContextFactory, TypeDbEnum contextType) : base(dbContextFactory, contextType)
    {
    }
}