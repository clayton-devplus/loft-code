using Loft.Code.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loft.Code.Domain.Interfaces.DbContext;

public interface IDbContextFactory
{
    IAppDbContext CreateDbContext(TypeDbEnum contextType);
    Task<IDbContextTransaction> BeginTransactionAsync(TypeDbEnum contextType);

}