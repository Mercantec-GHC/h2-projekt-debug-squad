using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Bookings.Handlers
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _repository;

        public GetAllBookingsHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookingDto>> Handle()
        {
            List<Booking> bookings = await _repository.GetAllAsync();

            return bookings.Select(b => new BookingDto(
                    b.Id,
                    new RoomDto(
                        b.Room.Id,
                        b.Room.Number,
                        b.Room.RoomType.Id,
                        b.Room.RoomType.Name,
                        b.Room.RoomType.Capacity,
                        b.Room.RoomType.PricePerNight
                    ),
                    b.CheckInDate,
                    b.CheckOutDate
            )).ToList();
        }
    }
}
