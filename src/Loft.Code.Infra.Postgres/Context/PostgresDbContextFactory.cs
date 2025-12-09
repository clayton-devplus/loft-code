using Loft.Code.Infra.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Loft.Code.Infra.Postgres.Context;

public class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
{
    public PostgresDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Host=localhost;Port=5432;Database=loftcode;Username=loft;Password=loft;";
        var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        optionsBuilder.UseNpgsql(connectionString,
                                                npgsqlOptions =>
                                                {
                                                    npgsqlOptions.CommandTimeout(380);
                                                    npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                                                });

        return new PostgresDbContext(optionsBuilder.Options);
    }
}
