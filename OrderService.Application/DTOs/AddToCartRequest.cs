namespace OrderService.Application.DTOs
{
    public class AddToCartRequest
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public string CardName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
