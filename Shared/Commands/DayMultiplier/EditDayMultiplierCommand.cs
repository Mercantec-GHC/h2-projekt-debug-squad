namespace Shared
{
    public class EditDayMultiplierCommand
    {
        public int Id { get; set; }
        public decimal Multiplier { get; set; }

        public EditDayMultiplierCommand() { }
        public EditDayMultiplierCommand(int id, decimal multiplier)
        {
            Id = id;
            Multiplier = multiplier;
        }
    }
}