using OrderService.Application.DTOs;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task AddToCartAsync(AddToCartRequest request);
        Task RemoveFromCartAsync(RemoveFromCartRequest request);

        Task ClearCartAsync(ClearCartRequest request);
    }
}
