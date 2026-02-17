using Application.Interfaces;
using Domain;
using Shared;

namespace Application.DayMultipliers.Handlers
{
    public class GetAllDayMultipliersHandler
    {
        private readonly IDayMultiplierRepository _repository;

        public GetAllDayMultipliersHandler(IDayMultiplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DayMultiplierDto>> Handle()
        {
            List<DayMultiplier> dayMultipliers = await _repository.GetAllAsync();

            return dayMultipliers.Select(d => new DayMultiplierDto(d.Id, d.Day, d.Multiplier)).ToList();
        }
    }
}
