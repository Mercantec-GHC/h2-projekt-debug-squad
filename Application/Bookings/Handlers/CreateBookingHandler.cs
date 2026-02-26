using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomPricingService _pricingService;

        public CreateBookingHandler(IGuestRepository guestRepository, IRoomTypeRepository roomTypeRepository, IBookingRepository bookingRepository, IRoomPricingService pricingService)
        {
            _guestRepository = guestRepository;
            _roomTypeRepository = roomTypeRepository;
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
        }

        public async Task Handle(CreateBookingCommand command)
        {
            RoomType roomType = await _roomTypeRepository.GetByIdAsync(command.RoomTypeId) ?? throw new ArgumentException("Room type not found");
            Guest guest = await _guestRepository.GetByIdAsync(command.GuestId) ?? throw new ArgumentException("Guest not found");

            var checkInUtc = DateTime.SpecifyKind(command.CheckInDate.Date, DateTimeKind.Utc);
            var checkOutUtc = DateTime.SpecifyKind(command.CheckOutDate.Date, DateTimeKind.Utc);

            if (checkInUtc.Date < DateTime.Today) throw new ArgumentException("Check-in date cannot be in the past");
            if (checkOutUtc <= checkInUtc) throw new ArgumentException("Check-out date must be after check-in date");

            int bookedBookings = await _roomTypeRepository.GetOverlappingAsync(command.RoomTypeId, checkInUtc, checkOutUtc);
            int totalRooms = await _roomTypeRepository.RoomCountAsync(command.RoomTypeId);

            if (bookedBookings >= totalRooms)
                throw new ArgumentException("No available rooms of this type for selected dates");



            decimal totalPrice = await _pricingService.CalculateTotalPriceAsync(roomType.PricePerNight, checkInUtc, checkOutUtc);

            var booking = new Booking(guest, roomType, checkInUtc, checkOutUtc, totalPrice);

            guest.AddBooking(booking);

            await _guestRepository.SaveChangesAsync();
        }
    }
}
