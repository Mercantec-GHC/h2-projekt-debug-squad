using Application.Guests.Queries;
using Application.Interfaces;
using Domain;
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
                    (
                        b.Room.Id,
                        b.Room.Number,
                        new RoomTypeDto(
                            b.Room.RoomType.Id,
                            b.Room.RoomType.Name,
                            b.Room.RoomType.Capacity,
                            b.Room.RoomType.PricePerNight
                        )
                    ),
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalPrice = b.TotalPrice
                }).ToList()
            };
        }


    }
}
