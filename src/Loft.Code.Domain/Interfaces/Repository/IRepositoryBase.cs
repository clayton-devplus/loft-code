using System.Linq.Expressions;

namespace Loft.Code.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T, TKey>
    {
        Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> selectFields, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? orderBy = null, Expression<Func<T, T>>? selectFields = null, params Expression<Func<T, object>>[] includes);
        Task<(IEnumerable<T> models, bool hasNext)> QueryListAsync(Expression<Func<T, bool>> filter, int page = 1, int pageSize = 10, string? search = "", Expression<Func<T, object>>? orderBy = null,
                                                                                        bool orderByDescending = false, params Expression<Func<T, object>>[] includes);
        public Task<T?> QueryFirsOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        public Task<IEnumerable<T>> QueryFromSqlRawAsync(string fromSqlRaw);
        Task<IEnumerable<TResult>> QueryFromSqlRawAsync<TResult>(string fromSqlRaw, params object[] parameters) where TResult : class;
        public Task<T?> GetByIdAsync(TKey id);
        public Task<bool> SaveAsync(T entity);
        public Task<bool> SaveRangeAsync(IEnumerable<T> entitiesToSave);

        public Task<bool> UpdateAsync(T entity);
        public Task<bool> SafeUpdateAsync(T entity);
        public Task<bool> DeleteAsync(T entityToDelete);
        public Task<bool> DeleteWithPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteRangeAsync(IEnumerable<T> entitiesToDelete);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    }
    public interface IRepositoryBase<T> : IRepositoryBase<T, long> where T : class
    {
    }
}