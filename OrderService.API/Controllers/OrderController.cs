using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Repositories;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            await _orderService.CheckoutAsync(request);
            return Ok("Sipariş oluşturuldu.");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("test-order")]
        public async Task<IActionResult> CreateTestOrder()
        {
            var order = new Order
            {
                Id = Guid.Parse("7be4c3e1-78dc-4c6f-84ee-4a984b88e290"),
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = new List<OrderItem>
        {
            new OrderItem
            {
                CardId = 1001,
                CardName = "Blue-Eyes White Dragon",
                Price = 50,
                Quantity = 2
            }
        }
            };

            await _orderService.CreateOrderAsync(order);
            return Ok("Test order created!");
        }
    }
}
