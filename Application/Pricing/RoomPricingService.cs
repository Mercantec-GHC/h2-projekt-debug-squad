using Application.Interfaces;
using Domain;

namespace Application.Pricing
{
    public class RoomPricingService : IRoomPricingService
    {
        private readonly IDayMultiplierRepository _dayMultiplierRepository;

        public RoomPricingService(IDayMultiplierRepository dayMultiplierRepository)
        {
            _dayMultiplierRepository = dayMultiplierRepository;
        }

        public async Task<decimal> CalculateTotalPriceAsync(decimal pricePerNight, DateTime checkIn, DateTime checkOut)
        {
            List<DayMultiplier> multipliers = await _dayMultiplierRepository.GetAllAsync();
            Dictionary<DayOfWeek, decimal> multiplierMap = multipliers.ToDictionary(m => m.Day, m => m.Multiplier);

            decimal total = 0m;

            for (DateTime date = checkIn.Date; date < checkOut.Date; date = date.AddDays(1))
            {
                decimal dayMultiplier = multiplierMap.GetValueOrDefault(date.DayOfWeek, 1.0m);
                total += pricePerNight * dayMultiplier;
            }

            return total;
        }
    }
}
