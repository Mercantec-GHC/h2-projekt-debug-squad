namespace Domain
{
    public class Booking
    {
        public int Id { get; private set; }
        public Guest Guest { get; private set; } = null!;
        public Room? Room { get; private set; }
        public RoomType RoomType { get; set; } = null!;
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }

        public decimal TotalPrice { get; private set; }

        private Booking() { }

        public Booking(Guest guest, RoomType roomType, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            Validate(guest, roomType, checkInDate, checkOutDate);

            Guest = guest;
            RoomType = roomType;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;
            TotalPrice = totalPrice;
        }

        private static void Validate(Guest guest, RoomType roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            ArgumentNullException.ThrowIfNull(guest, nameof(guest));
            ArgumentNullException.ThrowIfNull(roomType, nameof(roomType));


            if (checkInDate.Date >= checkOutDate.Date) throw new ArgumentException("Check-out date must be after check-in date");
        }

        public void ChangeDates(DateTime newCheckIn, DateTime newCheckOut, decimal totalPrice)
        {
            if (newCheckIn.Date >= newCheckOut.Date) throw new ArgumentException("Check-out date must be after check-in date");
            if (totalPrice < 0) throw new ArgumentException("Total price must be non-negative");

            CheckInDate = newCheckIn.Date;
            CheckOutDate = newCheckOut.Date;
            TotalPrice = totalPrice;
        }

        public void SetRoom(Room room)
        {
            Room = room;
        }
    }
}