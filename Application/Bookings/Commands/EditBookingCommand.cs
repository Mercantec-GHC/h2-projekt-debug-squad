namespace Application.Bookings.Commands
{
    public class EditBookingCommand
    {
        public int BookingId { get; init; }
        public int GuestId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
    }
}