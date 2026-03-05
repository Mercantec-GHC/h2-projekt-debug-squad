using Application.Guests.Queries;
using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Guests.Handlers
{
    public class GetGuestByEmailHandler
    {
        private readonly IGuestRepository _repository;

        public GetGuestByEmailHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<GuestDto?> Handle(GuestByEmailQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Email))
                return null;  

            var guest = await _repository.GetByEmailAsync(query.Email);

            if (guest == null)
                return null;

            return new GuestDto
            {
                Id = guest.Id,
                FullName = guest.FullName,
                PhoneNumber = guest.PhoneNumber,
                Email = guest.Email,
                Bookings = guest.Bookings.Select(b => new BookingDto
                {
                    Id = b.Id,
                    Room = b.Room != null ? new RoomDto
                    (
                        b.Room.Id,
                        b.Room.Number,
                        new RoomTypeDto(
                            b.Room.RoomType.Id,
                            b.Room.RoomType.Name,
                            b.Room.RoomType.Capacity,
                            b.Room.RoomType.PricePerNight
                        )
                    ) : null,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalPrice = b.TotalPrice
                }).ToList()
            };
        }
    }
}