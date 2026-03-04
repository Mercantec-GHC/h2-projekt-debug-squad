using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Commands.Booking
{
    public class AssignRoomCommand 
    { public int BookingId { get; set; } 
        public int RoomTypeId { get; set; } 
        // Optional: specific room id to assign. If provided, server will try to assign this room.
        public int RoomId { get; set; }
    
    }
}

