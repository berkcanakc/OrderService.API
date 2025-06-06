﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.BackgroundJobs;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Services;
//using OrderService.Application.Interfaces;
//using OrderService.Infrastructure.External;
//using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddHostedService<CartCleanupService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IStockClientService, StockApiClient>();
            services.AddHttpClient<IStockClientService, StockApiClient>();
            // İleride: Retry policies eklenebilir.

            return services;
        }
    }
}
