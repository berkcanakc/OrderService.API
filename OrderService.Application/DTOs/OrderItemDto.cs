namespace OrderService.Application.DTOs
{
    public class OrderItemDto
    {
        public int CardId { get; set; }
        public string CardName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
}
