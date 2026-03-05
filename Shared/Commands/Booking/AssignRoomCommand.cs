using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Commands.Booking
{
    public class AssignRoomCommand
    {
        public int BookingId { get; set; }
        public int RoomTypeId { get; set; }

        public AssignRoomCommand(int bookingId, int roomTypeId)
        {
            BookingId = bookingId;
            RoomTypeId = roomTypeId;
        }
    }
}

