namespace Shared
{
    public class RegisterGuestCommand
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public RegisterGuestCommand() { }
        public RegisterGuestCommand(string fullName, string phoneNumber, string email)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
