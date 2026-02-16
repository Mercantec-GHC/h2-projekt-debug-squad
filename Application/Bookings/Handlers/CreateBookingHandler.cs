using Application.Bookings.Commands;
using Application.Interfaces;
using Domain;

namespace Application.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public CreateBookingHandler(
            IGuestRepository guestRepository,
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(CreateBookingCommand command)
        {
            // Get room
            var room = await _roomRepository.GetByIdAsync(command.RoomId);
            if (room == null) throw new ArgumentException("Room not found");

            // Get guest
            var guest = await _guestRepository.GetByIdAsync(command.GuestId);
            if (guest == null) throw new ArgumentException("Guest not found");

            if (command.CheckInDate.Date < DateTime.Today)
                throw new ArgumentException("Check-in date cannot be in the past");

            // Validate dates
            if (command.CheckOutDate <= command.CheckInDate)
                throw new ArgumentException("Check-out date must be after check-in date");

            // Get all bookings
            var allBookings = await _bookingRepository.GetAllAsync();

            // Check if room is already booked for requested dates
            bool isRoomBooked = allBookings.Any(b =>
                b.Room.Id == room.Id &&
                b.CheckInDate.Date < command.CheckOutDate.Date &&
                b.CheckOutDate.Date > command.CheckInDate.Date
            );

            if (isRoomBooked)
                throw new ArgumentException("Room is already booked for the selected dates");

            var checkInUtc = DateTime.SpecifyKind(command.CheckInDate.Date, DateTimeKind.Utc);
            var checkOutUtc = DateTime.SpecifyKind(command.CheckOutDate.Date, DateTimeKind.Utc);

            // Create booking
            var booking = new Booking(guest, room, checkInUtc, checkOutUtc);

            // Add booking to guest
            guest.AddBooking(booking);

            // Save changes
            await _guestRepository.SaveChangesAsync();
        }
    }
}
