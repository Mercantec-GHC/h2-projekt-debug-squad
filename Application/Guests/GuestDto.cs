using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Bookings;

namespace Application.Guests.Queries
{
    public class GuestDto
    {
        public int Id { get; private set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<BookingDto> Bookings { get; set; } = new();

        private GuestDto() { }

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

