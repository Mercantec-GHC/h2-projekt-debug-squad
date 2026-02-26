namespace Shared
{
    public class GetAvailableRoomTypesCommand
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Capacity { get; set; }
        public decimal MaxPrice { get; set; }

        public GetAvailableRoomTypesCommand() { }

        public GetAvailableRoomTypesCommand(DateTime checkInDate, DateTime checkOutDate, int capacity, decimal maxPrice) 
        { 
            Capacity = capacity;
            MaxPrice = maxPrice;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }
    }
}
