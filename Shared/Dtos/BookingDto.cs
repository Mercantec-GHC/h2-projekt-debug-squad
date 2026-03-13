namespace Shared
{
    public class BookingDto
    {
        public int Id { get; set; }
        public RoomDto? Room { get; set; }
        public GuestDto? Guest { get; set; }
        public RoomTypeDto RoomType { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public BookingDto() { }

        public BookingDto(int id, RoomDto? room, RoomTypeDto roomType, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            Id = id;
            Room = room;
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }

        public BookingDto(int id, RoomDto? room, GuestDto guest, RoomTypeDto roomType, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            Id = id;
            Room = room;
            Guest = guest;
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }

        public BookingDto(int id, RoomTypeDto roomType, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            Id = id;
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }
    }
}
