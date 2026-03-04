namespace Shared
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public LoginResponseDto() { }
        public LoginResponseDto(string token, string fullName)
        {
            Token = token;
            FullName = fullName;
        }
    }
}
