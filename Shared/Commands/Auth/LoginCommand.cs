namespace Shared
{
    public class LoginCommand
    {
        public string Email { get; set; } = string.Empty;

        public LoginCommand() { }
        public LoginCommand(string email)
        {
            Email = email;
        }
    }
}
