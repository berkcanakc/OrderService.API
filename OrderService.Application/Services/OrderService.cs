using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }
        public async Task CheckoutAsync(CheckoutRequest request)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
            if (cart == null || cart.Items.Count == 0)
                throw new Exception("Sepet boş.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = cart.Items.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    CardId = i.CardId,
                    CardName = i.CardName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    TotalPrice = i.Quantity * i.Price
                }).ToList()
            };

            await _orderRepository.CreateOrderAsync(order);
            await _cartRepository.DeleteCartAsync(cart.Id);
        }

    }
}
