using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OrderService.Infrastructure.DependencyInjection;
//using OrderService.Application.Interfaces;
//using OrderService.Application.Services;

namespace OrderService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddPersistence(configuration);
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IOrderStatusHistoryService, OrderStatusHistoryService>();

            return services;
        }
    }
}
