using System;
using System.Collections.Generic;
using System.Text;


namespace Shared.Commands.Booking
{
    public class ReassignRoomCommand
    {
        public int BookingId { get; set; }
        public int RoomTypeId { get; set; }
    }
}