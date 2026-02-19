namespace Shared
{
    public class BookingDto
    {
        public int Id { get; set; }
        public RoomDto Room { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public BookingDto() { }

        public BookingDto(int id, RoomDto room, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public BookingDto(int id, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
