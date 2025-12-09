using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loft.Code.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICalculadoraAluguelService, Services.CalculadoraAluguelService>();
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}