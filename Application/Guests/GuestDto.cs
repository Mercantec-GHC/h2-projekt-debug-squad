using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Queries
{
    public class GuestDto
    {
        public int Id { get; private set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        private GuestDto() { }

        public GuestDto(int id, string fullName, string phoneNumber, string email)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}

