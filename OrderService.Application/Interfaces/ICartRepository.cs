using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task SaveCartAsync(Cart cart);
        Task DeleteCartAsync(Guid cartId);
    }
}
