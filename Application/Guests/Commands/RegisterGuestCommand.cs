using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Commands
{
    public class RegisterGuestCommand
    {
        public string FullName { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
