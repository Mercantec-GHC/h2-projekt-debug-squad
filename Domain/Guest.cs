using System;
using System.Collections.Generic;
using System.Text;


namespace Domain
{
    public class Guest
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        


        
        public List<Booking> Bookings { get; private set; } = new();

        private Guest() { }
        public Guest(string fullName, string phoneNumber, string email)
        {
            Validate(fullName, phoneNumber, email);

            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void Change(string fullName, string phoneNumber, string email)
        {
            Validate(fullName, phoneNumber, email);

            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public void AddBooking(Booking booking)
        {
            Bookings.Add(booking);
        }
        public void RemoveBooking(Booking booking)
        {
            Bookings.Remove(booking);
        }
        private static void Validate(string fullName, string phoneNumber, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name is required", nameof(fullName));

            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Phone number is required", nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
        }
    }
}
