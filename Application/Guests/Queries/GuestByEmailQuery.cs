using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Queries
{
    public class GuestByEmailQuery
    {
        public string Email { get; set; }

        public GuestByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
