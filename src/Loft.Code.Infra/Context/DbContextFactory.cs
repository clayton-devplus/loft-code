
using Loft.Code.Domain.Enums;
using Loft.Code.Domain.Interfaces.DbContext;
using Loft.Code.Infra.Postgres.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Loft.Code.Infra.Context;

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAppDbContext CreateDbContext(TypeDbEnum contextType)
    {
        return contextType switch
        {
            TypeDbEnum.Application => _serviceProvider.GetRequiredService<PostgresDbContext>(),
            TypeDbEnum.Public => _serviceProvider.GetRequiredService<PostgresDbContext>(),
            _ => throw new ArgumentException($"Context type '{contextType}' is not recognized.")
        };
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync(TypeDbEnum contextType)
    {
        var context = CreateDbContext(contextType);
        return await context.BeginTransactionAsync();
    }
}