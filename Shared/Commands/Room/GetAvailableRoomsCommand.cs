namespace Shared
{
    public class GetAvailableRoomsCommand
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int? Capacity { get; set; }
        public decimal? MaxPrice { get; set; }

        public GetAvailableRoomsCommand() { }
        public GetAvailableRoomsCommand(DateTime checkInDate, DateTime checkOutDate, int? capacity, decimal? maxPrice)
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Capacity = capacity;
            MaxPrice = maxPrice;
        }
    }
}
