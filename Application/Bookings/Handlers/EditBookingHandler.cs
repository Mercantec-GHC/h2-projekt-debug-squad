using Application.Bookings.Commands;
using Application.Interfaces;

namespace Application.Bookings.Handlers
{
    public class EditBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public EditBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> Handle(EditBookingCommand command)
        {
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId);
            if (booking == null) return false;

            if (command.CheckInDate.Date < DateTime.Today)
                throw new ArgumentException("Check-in date cannot be in the past");

            // Check for conflicting bookings on the same room
            var allBookings = await _bookingRepository.GetAllAsync();
            bool isRoomBooked = allBookings.Any(b =>
                b.Room.Id == booking.Room.Id &&
                b.Id != booking.Id &&
                b.CheckInDate.Date < command.CheckOutDate.Date &&
                b.CheckOutDate.Date > command.CheckInDate.Date
            );

            if (isRoomBooked)
                throw new ArgumentException("Room is already booked for the selected dates");

            var checkInUtc = DateTime.SpecifyKind(command.CheckInDate.Date, DateTimeKind.Utc);
            var checkOutUtc = DateTime.SpecifyKind(command.CheckOutDate.Date, DateTimeKind.Utc);

            booking.ChangeDates(checkInUtc, checkOutUtc);

            await _bookingRepository.SaveChangesAsync();
            return true;
        }
    }
}