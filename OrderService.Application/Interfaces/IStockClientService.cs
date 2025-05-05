namespace OrderService.Application.Interfaces
{
    public interface IStockClientService
    {
        Task ReleaseReservedStockAsync(int cardId, int quantity);
        // İleride: Task ReserveStockAsync(...) vs.
    }
}
