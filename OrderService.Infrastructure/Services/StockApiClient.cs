using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.Services
{
    public class StockApiClient : IStockClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StockApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task ReleaseReservedStockAsync(int cardId, int quantity)
        {
            var request = new
            {
                cardId,
                quantity
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var baseUrl = _configuration["GatewayBaseUrl"]; // örn: https://cardservice.local/api/
            var response = await _httpClient.PostAsync($"{baseUrl}/card/stock/release", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
