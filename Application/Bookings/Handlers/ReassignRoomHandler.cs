using Application.Interfaces;
using Shared.Commands.Booking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Bookings.Handlers
{
    public class ReassignRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public ReassignRoomHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(ReassignRoomCommand command)
        {
            if (command.BookingId <= 0)
                throw new ArgumentException("Invalid Booking ID.");
            if (command.RoomTypeId <= 0)
                throw new ArgumentException("Invalid Room Type ID.");

            var booking = await _bookingRepository.GetByIdAsync(command.BookingId);
            if (booking == null)
                throw new KeyNotFoundException("Booking not found.");

            if (booking.Room == null)
                throw new InvalidOperationException("Booking has no room assigned yet. Use AssignRoom first.");

            // Fetch available rooms excluding the current assigned room
            var availableRooms = await _roomRepository.GetAvailableAsync(
                booking.CheckInDate,
                booking.CheckOutDate,
                capacity: null,
                maxPrice: null
            );

            var roomToAssign = availableRooms
                .Where(r => r.RoomType.Id == command.RoomTypeId && r.Id != booking.Room.Id)
                .OrderBy(r => int.TryParse(r.Number, out var n) ? n : int.MaxValue)
                .FirstOrDefault();

            if (roomToAssign == null)
                throw new InvalidOperationException("No available rooms to reassign for the selected room type.");

            // Reassign room
            booking.SetRoom(roomToAssign);
            await _bookingRepository.UpdateAsync(booking);
        }
    }
}
