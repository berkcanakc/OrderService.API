namespace OrderService.Application.DTOs
{
    public class RemoveFromCartRequest
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
    }
}
