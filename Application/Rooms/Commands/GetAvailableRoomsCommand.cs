namespace Application.Rooms.Commands
{
    public class GetAvailableRoomsCommand
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int? Capacity { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
