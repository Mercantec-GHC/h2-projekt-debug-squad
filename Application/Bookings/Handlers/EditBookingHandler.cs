using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Bookings.Handlers
{
    public class EditBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomPricingService _pricingService;

        public EditBookingHandler(IBookingRepository bookingRepository, IRoomPricingService pricingService)
        {
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
        }

        public async Task Handle(EditBookingCommand command)
        {
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId) ?? throw new ArgumentException("Booking not found");

            if (command.CheckInDate.Date < DateTime.Today) throw new ArgumentException("Check-in date cannot be in the past");

            var allBookings = await _bookingRepository.GetAllAsync();
            bool isRoomBooked = allBookings.Any(b =>
                b.Room.Id == booking.Room.Id &&
                b.Id != booking.Id &&
                b.CheckInDate.Date < command.CheckOutDate.Date &&
                b.CheckOutDate.Date > command.CheckInDate.Date
            );

            if (isRoomBooked) throw new ArgumentException("Room is already booked for the selected dates");

            var checkInUtc = DateTime.SpecifyKind(command.CheckInDate.Date, DateTimeKind.Utc);
            var checkOutUtc = DateTime.SpecifyKind(command.CheckOutDate.Date, DateTimeKind.Utc);

            decimal totalPrice = await _pricingService.CalculateTotalPriceAsync(booking.Room.RoomType.PricePerNight, checkInUtc, checkOutUtc);

            booking.ChangeDates(checkInUtc, checkOutUtc, totalPrice);

            await _bookingRepository.SaveChangesAsync();
        }
    }
}
