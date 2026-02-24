namespace Shared
{
    public class CreateBookingCommand
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public CreateBookingCommand() { }
        public CreateBookingCommand(int guestId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            GuestId = guestId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
