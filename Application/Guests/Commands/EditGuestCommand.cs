using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Commands
{
    public class EditGuestCommand
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


    }
}
