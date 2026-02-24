namespace Shared
{
    public class DeleteBookingCommand
    {
        public int GuestId { get; set; }
        public int BookingId { get; set; }

        public DeleteBookingCommand() { }
        public DeleteBookingCommand(int guestId, int bookingId)
        {
            GuestId = guestId;
            BookingId = bookingId;
        }
    }
}
