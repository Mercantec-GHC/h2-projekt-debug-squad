using Application.Interfaces;
using Domain;
using Shared;

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
            var booking = await _repository.GetByIdAsync(id);

            if (booking is null)
                return null;

            return new BookingDto(booking.Id, new RoomDto(booking.Room.Id, booking.Room.Number, new RoomTypeDto(booking.Room.RoomType.Id, booking.Room.RoomType.Name, booking.Room.RoomType.Capacity, booking.Room.RoomType.PricePerNight)), booking.CheckInDate, booking.CheckOutDate, booking.TotalPrice);
        }
    }
}
