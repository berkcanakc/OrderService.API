using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Persistence.Contexts;

namespace OrderService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OrderDbContext _context;

        public CartRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task SaveCartAsync(Cart cart)
        {
            var existing = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existing == null)
            {
                await _context.Carts.AddAsync(cart);
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(cart);

                // Güncellenen item listesini yeniden yaz
                existing.Items.Clear();
                foreach (var item in cart.Items)
                {
                    existing.Items.Add(item);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Cart>> GetExpiredCartsAsync(TimeSpan expiration)
        {
            var threshold = DateTime.UtcNow - expiration;

            return await _context.Carts
                .Include(c => c.Items)
                .Where(c => c.UpdatedAt < threshold)
                .ToListAsync();
        }

    }
}
