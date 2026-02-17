namespace Application.DayMultipliers
{
    public class DayMultiplierDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public decimal Multiplier { get; set; }

        public DayMultiplierDto() { }

        public DayMultiplierDto(int id, DayOfWeek day, decimal multiplier)
        {
            Id = id;
            Day = day;
            Multiplier = multiplier;
        }
    }
}
