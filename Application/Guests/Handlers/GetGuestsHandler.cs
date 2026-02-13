using Application.Bookings;
using Application.Guests.Queries;
using Application.Guests.Queries;
using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Handlers
{
    public class GetGuestsHandler
    {
        private readonly IGuestRepository _repository;

        public GetGuestsHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GuestDto>> Handle()
        {
            List<Guest> guests = await _repository.GetAllAsync();
            return guests.Select(g => new GuestDto(
                g.Id,
                g.FullName,
                g.PhoneNumber,
                g.Email,
                g.Bookings.Select(b => new BookingDto(
                    b.Id,
                    new RoomDto(
                        b.Room.Id,
                        b.Room.Number,
                        b.Room.Capacity,
                        b.Room.PricePerNight,
                        b.Room.IsAvailable
                    ),
                    b.CheckInDate,
                    b.CheckOutDate
                )).ToList()
            )).ToList();
        }
    }
}

