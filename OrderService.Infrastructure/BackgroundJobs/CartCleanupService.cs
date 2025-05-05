using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.BackgroundJobs
{
    public class CartCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CartCleanupService> _logger;

        public CartCleanupService(IServiceProvider serviceProvider, ILogger<CartCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var cartRepository = scope.ServiceProvider.GetRequiredService<ICartRepository>();
                    var stockClient = scope.ServiceProvider.GetRequiredService<IStockClientService>();

                    var expiredCarts = await cartRepository.GetExpiredCartsAsync(TimeSpan.FromMinutes(15));

                    foreach (var cart in expiredCarts)
                    {
                        foreach (var item in cart.Items)
                        {
                            await stockClient.ReleaseReservedStockAsync(item.CardId, item.Quantity);
                        }

                        await cartRepository.DeleteCartAsync(cart.Id);
                        _logger.LogInformation($"Sepet silindi: {cart.Id}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Sepet temizleme sırasında hata oluştu.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // her dakika çalışır
            }
        }
    }
}
