using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;

namespace Application.Bookings.Handlers
{
    public class GetBookingByIdHandler
    {
        private readonly IBookingRepository _repository;

        public GetBookingByIdHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookingDto?> Handle(int id)
        {
            Booking? booking = await _repository.GetByIdAsync(id);

            if (booking is null)
                return null;
            
            return new BookingDto(
                booking.Id,
                new RoomDto(
                    booking.Room.Id,
                    booking.Room.Number,
                    booking.Room.Capacity,
                    booking.Room.PricePerNight,
                    booking.Room.IsAvailable
                ),
                booking.CheckInDate,
                booking.CheckOutDate
            );
        }
    }
}
