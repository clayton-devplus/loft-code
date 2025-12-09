using Loft.Code.Domain.Enums;
using Loft.Code.Domain.Interfaces.DbContext;
using Loft.Code.Domain.Repository;

namespace Loft.Code.Infra.Repository
{
    public class GenericRepository<T> : RepositoryBase<T> where T : class
    {
        public GenericRepository(IDbContextFactory contextFactory)
            : base(contextFactory, TypeDbEnum.Application) { }
    }

    public class GenericRepository<T, TKey> : RepositoryBase<T, TKey> where T : class
    {
        public GenericRepository(IDbContextFactory contextFactory)
            : base(contextFactory, TypeDbEnum.Public) { }
    }
}
