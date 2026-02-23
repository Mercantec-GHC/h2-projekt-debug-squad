namespace Shared
{
    public class CreateBookingCommand
    {
        public int GuestId { get; init; }
        public int RoomId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
    }
}
