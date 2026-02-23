using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Guests.Handlers
{
    public class GetAllGuestsHandler
    {
        private readonly IGuestRepository _repository;

        public GetAllGuestsHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GuestDto>> Handle()
        {
            List<Guest> guests = await _repository.GetAllAsync();
            return guests.Select(g => new GuestDto(g.Id, g.FullName, g.PhoneNumber, g.Email, g.Bookings.Select(b => new BookingDto(b.Id, b.CheckInDate, b.CheckOutDate, b.TotalPrice)).ToList())).ToList();
        }
    }
}
