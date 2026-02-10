using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public Room Room { get; set; } = null!;

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        private BookingDto() { }

        public BookingDto(int id, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
