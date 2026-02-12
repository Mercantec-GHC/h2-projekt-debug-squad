using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Commands
{
    public class GetAvailableRoomsCommand
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
