using Application.Interfaces;
using Shared.Commands.Booking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Bookings.Handlers
{
    public class AssignRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public AssignRoomHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(AssignRoomCommand command)
        {
            if (command.BookingId <= 0)
                throw new ArgumentException("Invalid Booking ID.");
            if (command.RoomTypeId <= 0)
                throw new ArgumentException("Invalid Room Type ID.");

            // Fetch the booking
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found.");

            // Fetch available rooms for the booking dates
            var availableRooms = await _roomRepository.GetAvailableAsync(
                booking.CheckInDate,
                booking.CheckOutDate,
                capacity: null,
                maxPrice: null
            );

            // Filter rooms by RoomTypeId and pick the lowest-numbered room
            var roomToAssign = availableRooms
                .Where(r => r.RoomType.Id == command.RoomTypeId)
                .OrderBy(r => int.TryParse(r.Number, out var n) ? n : int.MaxValue)
                .FirstOrDefault();

            if (roomToAssign == null)
                throw new InvalidOperationException("No available rooms for the selected room type on the given dates.");

            // Assign the room using the entity method
            booking.SetRoom(roomToAssign);

            // Save the changes
            await _bookingRepository.UpdateAsync(booking);
        }
    }
}