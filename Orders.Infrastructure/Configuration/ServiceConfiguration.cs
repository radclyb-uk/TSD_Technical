

using Microsoft.Extensions.DependencyInjection;
using Orders.Contracts.Infrastructure;
using Orders.Infrastructure.Contexts;
using Orders.Infrastructure.Repositories;

namespace Orders.Infrastructure.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureInjection(this IServiceCollection services)
        {
            services.AddDbContext<OrderContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
