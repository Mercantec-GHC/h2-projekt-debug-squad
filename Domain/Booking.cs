namespace Domain
{
    public class Booking
    {
        public int Id { get; private set; }
        public Guest Guest { get; private set; } = null!;
        public Room Room { get; private set; } = null!;

        public DateTime CheckInDate { get; private set; }

        public DateTime CheckOutDate { get; private set; }

        // calculated property
        // result of CheckOutDate - CheckInDate) = timespan
        // timespan.TotalDays = number of days
        public decimal TotalPrice => Room.RoomType.PricePerNight * (decimal)(CheckOutDate - CheckInDate).TotalDays;

        private Booking() { }

        public Booking(Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            Validate(guest, room, checkInDate, checkOutDate);

            Guest = guest;
            Room = room;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;
        }

        private static void Validate(Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            ArgumentNullException.ThrowIfNull(guest, nameof(guest));
            ArgumentNullException.ThrowIfNull(room, nameof(room));

            if (checkInDate.Date >= checkOutDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date");
        }

        public void ChangeDates(DateTime newCheckIn, DateTime newCheckOut)
        {
            if (newCheckIn.Date >= newCheckOut.Date)
                throw new ArgumentException("Check-out date must be after check-in date");

            CheckInDate = newCheckIn.Date;
            CheckOutDate = newCheckOut.Date;
        }
    }
}