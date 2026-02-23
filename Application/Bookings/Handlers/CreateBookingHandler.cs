using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomPricingService _pricingService;

        public CreateBookingHandler(IGuestRepository guestRepository, IRoomRepository roomRepository, IBookingRepository bookingRepository, IRoomPricingService pricingService)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
        }

        public async Task Handle(CreateBookingCommand command)
        {
            Room room = await _roomRepository.GetByIdAsync(command.RoomId) ?? throw new ArgumentException("Room not found");

            Guest guest = await _guestRepository.GetByIdAsync(command.GuestId) ?? throw new ArgumentException("Guest not found");

            if (command.CheckInDate.Date < DateTime.Today) throw new ArgumentException("Check-in date cannot be in the past");

            if (command.CheckOutDate <= command.CheckInDate) throw new ArgumentException("Check-out date must be after check-in date");

            var allBookings = await _bookingRepository.GetAllAsync();

            bool isRoomBooked = allBookings.Any(b => b.Room.Id == room.Id && b.CheckInDate.Date < command.CheckOutDate.Date && b.CheckOutDate.Date > command.CheckInDate.Date);

            if (isRoomBooked) throw new ArgumentException("Room is already booked for the selected dates");

            var checkInUtc = DateTime.SpecifyKind(command.CheckInDate.Date, DateTimeKind.Utc);
            var checkOutUtc = DateTime.SpecifyKind(command.CheckOutDate.Date, DateTimeKind.Utc);

            decimal totalPrice = await _pricingService.CalculateTotalPriceAsync(room.RoomType.PricePerNight, checkInUtc, checkOutUtc);

            var booking = new Booking(guest, room, checkInUtc, checkOutUtc, totalPrice);

            guest.AddBooking(booking);

            await _guestRepository.SaveChangesAsync();
        }
    }
}
