namespace Shared
{
    public class EditBookingCommand
    {
        public int BookingId { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public EditBookingCommand() { }
        public EditBookingCommand(int bookingId, int guestId, DateTime checkInDate, DateTime checkOutDate)
        {
            BookingId = bookingId;
            GuestId = guestId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}