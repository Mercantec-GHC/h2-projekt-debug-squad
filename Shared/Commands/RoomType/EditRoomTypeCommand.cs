namespace Shared
{
    public class EditRoomTypeCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }

        public EditRoomTypeCommand() { }
        public EditRoomTypeCommand(int id, string name, int capacity, decimal pricePerNight)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }
    }
}
