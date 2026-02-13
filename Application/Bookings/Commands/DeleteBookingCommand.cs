using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings.Commands
{
    public class DeleteBookingCommand
    {
        public int GuestId { get; set; }
        public int BookingId { get; set; }
    }
}
