using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task AddToCartAsync(AddToCartRequest request)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Items = new List<CartItem>()
                };
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.CardId == request.CardId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    Id = Guid.NewGuid(),
                    CardId = request.CardId,
                    CardName = request.CardName,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    CartId = cart.Id
                });
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _cartRepository.SaveCartAsync(cart);
        }

        public async Task RemoveFromCartAsync(RemoveFromCartRequest request)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
                return;

            var item = cart.Items.FirstOrDefault(i => i.CardId == request.CardId);
            if (item != null)
            {
                cart.Items.Remove(item);
                if (!cart.Items.Any())
                {
                    await _cartRepository.DeleteCartAsync(cart.Id); // 🔥 Sepeti sil
                }
                else
                {
                    cart.UpdatedAt = DateTime.UtcNow;
                    await _cartRepository.SaveCartAsync(cart);
                }
            }
        }

        public async Task ClearCartAsync(ClearCartRequest request)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
            if (cart != null)
            {
                await _cartRepository.DeleteCartAsync(cart.Id);
            }
        }

    }
}
