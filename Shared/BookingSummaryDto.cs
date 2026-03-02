using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class BookingSummaryDto
    {
        public int Id { get; set; }
        public RoomDto? Room { get; set; } = null!;
        public RoomTypeDto? RoomType { get; set; } = null!;
        public string GuestName { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Parameterless constructor 
        public BookingSummaryDto() { }

        // Full constructor
        public BookingSummaryDto(int id, RoomDto? room, string guestName, RoomTypeDto roomType, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            Id = id;
            Room = room;
            GuestName = guestName;
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }
    }
}
