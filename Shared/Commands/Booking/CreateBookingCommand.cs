namespace Shared;

public class CreateBookingCommand
{
    public int GuestId { get; set; }
    public int RoomTypeId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public CreateBookingCommand() { }
    public CreateBookingCommand(int guestId, int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
    {
        GuestId = guestId;
        RoomTypeId = roomTypeId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }
}
