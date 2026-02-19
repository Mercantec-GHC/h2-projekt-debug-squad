using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Bookings.Handlers
{
    public class GetBookingsByGuestIdHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetBookingsByGuestIdHandler(IGuestRepository repository)
        {
            _guestRepository = repository;
        }

        public async Task<List<BookingDto>?> Handle(int guestId)
        {
            Guest? guest = await _guestRepository.GetByIdAsync(guestId);

            if (guest is null)
                return null;

            return guest.Bookings.Select(b => new BookingDto(
                b.Id,
                new RoomDto(
                    b.Room.Id,
                    b.Room.Number,
                    new RoomTypeDto(
                        b.Room.RoomType.Id,
                        b.Room.RoomType.Name,
                        b.Room.RoomType.Capacity,
                        b.Room.RoomType.PricePerNight
                    )
                ),
                b.CheckInDate,
                b.CheckOutDate
                )).ToList();
        }
    }
}
