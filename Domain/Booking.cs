using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Booking
    {
        public int Id { get; private set; }
        public Guest Guest { get; private set; } = null!;
        public Room Room { get; private set; } = null!;

        public DateTime CheckInDate { get; private set; }

        public DateTime CheckOutDate { get; private set; }

        // calculated property
        // result of CheckOutDate - CheckInDate) = timespan
        // timespan.TotalDays = number of days
        public decimal TotalPrice => Room.PricePerNight * (decimal)(CheckOutDate - CheckInDate).TotalDays;

        private Booking() { }

        public Booking(Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            Validate(guest, room, checkInDate, checkOutDate);

            Guest = guest;
            Room = room;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;

            room.MarkAsUnavailable();

            guest.Bookings?.Add(this);
        }

        private static void Validate(Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            ArgumentNullException.ThrowIfNull(guest, nameof(guest));
            ArgumentNullException.ThrowIfNull(room, nameof(room));

            if (checkInDate.Date >= checkOutDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date");
            //if (!room.IsAvailable)
            //    throw new InvalidOperationException("Room is not available");
        }

        public void Cancel()
        {
            Room.MarkAsAvailable();
            Guest.Bookings?.Remove(this);
        }
    }
}