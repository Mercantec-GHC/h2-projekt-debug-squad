using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings.Commands
{
    public class CreateBookingCommand
    {
        public int GuestId { get; init; }

        public int RoomID { get; init; }

        public DateTime CheckInDate { get; init; }

        public DateTime CheckOutDate { get; init; }
    }
}
