using Loft.Code.Domain.Interfaces.DbContext;
using Loft.Code.Domain.Interfaces.Repository;
using Loft.Code.Infra.Context;
using Loft.Code.Infra.Postgres.Context;
using Loft.Code.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loft.Code.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings")["PublicConnection"] ?? "";

            services.AddDbContext<PostgresDbContext>(options =>
                                       options.UseNpgsql(connectionString,
                                        sqlOptions =>
                                        {
                                            sqlOptions.MigrationsAssembly(typeof(PostgresDbContext).Assembly.FullName);
                                            sqlOptions.CommandTimeout(380);
                                            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                                        }));


            services.AddScoped<IDbContextFactory, DbContextFactory>();
            services.AddScoped<IAppDbContext, PostgresDbContext>();

            services.AddScoped(typeof(IRepositoryBase<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IRepositoryBase<,>), typeof(GenericRepository<,>));


            return services;
        }
    }
}