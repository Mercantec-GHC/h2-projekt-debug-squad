using Application.Interfaces;
using Application.Bookings.Commands;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateBookingHandler(IGuestRepository guestRepository, IRoomRepository roomRepository)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
        }

        public async Task Handle(CreateBookingCommand command)
        {
            //  Get the Room
            var room = await _roomRepository.GetByIdAsync(command.RoomId);
            if (room == null)
                throw new ArgumentException("Room not found");

            //  Get the Guest
            var guest = await _guestRepository.GetByIdAsync(command.GuestId);
            if (guest == null)
                throw new ArgumentException("Guest not found");

            //  Validate dates
            if (command.CheckOutDate <= command.CheckInDate)
                throw new ArgumentException("Check-out date must be after check-in date");

            //  Convert dates to UTC
            var checkInUtc = command.CheckInDate.ToUniversalTime();
            var checkOutUtc = command.CheckOutDate.ToUniversalTime();

            //  Create the booking
            var booking = new Booking(guest, room, checkInUtc, checkOutUtc);

            // Add booking to guest
            guest.AddBooking(booking);

            //  Save changes
            await _guestRepository.SaveChangesAsync();
        }
    }
}
