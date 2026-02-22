namespace Application.Interfaces
{
    public interface IRoomPricingService
    {
        Task<decimal> CalculateTotalPriceAsync(decimal pricePerNight, DateTime checkIn, DateTime checkOut);
    }
}
