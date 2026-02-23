namespace Shared
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<BookingDto> Bookings { get; set; } = new();

        public GuestDto() { }

        public GuestDto(int id, string fullName, string phoneNumber, string email, List<BookingDto> bookings)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
            Bookings = bookings;
        }
    }
}
