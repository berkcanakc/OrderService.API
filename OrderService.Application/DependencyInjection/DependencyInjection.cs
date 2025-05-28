using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
//using OrderService.Application.Interfaces;
//using OrderService.Application.Services;

namespace OrderService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, Services.OrderService>();
            //services.AddScoped<IOrderStatusHistoryService, OrderStatusHistoryService>();

            return services;
        }
    }
}
