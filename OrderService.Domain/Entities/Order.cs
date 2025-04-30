using System.ComponentModel.DataAnnotations.Schema;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
    }
}
