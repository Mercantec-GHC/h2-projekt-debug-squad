using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Booking
    {
        public int Id { get; private set; }

        public Guest Guest { get; private set; }

        public Room Room { get; private set; }

        public DateTime CheckInDate { get; private set; }

        public DateTime CheckOutDate { get; private set; }

        public decimal TotalPrice => Room.PricePerNight * (decimal)(CheckOutDate - CheckInDate).TotalDays;

        private Booking() { }  

        public Booking(Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            if (guest == null) throw new ArgumentNullException(nameof(guest));
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (checkInDate.Date >= checkOutDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date");
            if (!room.IsAvailable)
                throw new InvalidOperationException("Room is not available");

            Guest = guest;
            Room = room;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;

            room.MarkAsUnavailable();

            guest.Bookings?.Add(this);
        }
    }
}