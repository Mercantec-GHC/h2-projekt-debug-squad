using Application.Guests.Queries;
using Application.Interfaces;
using Shared;

namespace Application.Guests.Handlers
{
    public class GetGuestByIdHandler
    {
        private readonly IGuestRepository _repository;

        public GetGuestByIdHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<GuestDto?> Handle(GuestByIdQuery query)
        {
            var guest = await _repository.GetByIdAsync(query.Id);

            if (guest == null)
                return null;

            // Map entity to DTO
            return new GuestDto(
       guest.Id,
       guest.FullName,
       guest.PhoneNumber,
       guest.Email,
       guest.Bookings.Select(b => new BookingDto(
           b.Id,
           new RoomDto(
               b.Room.Id,
               b.Room.Number,
               b.Room.Capacity,
               b.Room.PricePerNight
           ),
           b.CheckInDate,
           b.CheckOutDate
       )).ToList()
   );

        }
    }
}
