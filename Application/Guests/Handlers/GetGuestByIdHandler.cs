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
            return new GuestDto
            {
                Id = guest.Id,
                FullName = guest.FullName,
                PhoneNumber = guest.PhoneNumber,
                Email = guest.Email,
                Bookings = guest.Bookings.Select(b => new BookingDto
                {
                    Id = b.Id,
                    Room = new RoomDto
                    {
                        Id = b.Room.Id,
                        Number = b.Room.Number,
                        Capacity = b.Room.RoomType.Capacity,
                        PricePerNight = b.Room.RoomType.PricePerNight
                    },
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate
                }).ToList()
            };
        }


    }
}
