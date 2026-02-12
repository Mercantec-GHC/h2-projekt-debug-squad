using Application.Bookings.Commands;
using Application.Guests.Queries;
using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings.Handlers
{
    public class GetBookingsByGuestIdHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetBookingsByGuestIdHandler(IGuestRepository repository)
        {
            _guestRepository = repository;
        }

        public async Task<List<BookingDto>?> Handle(int guestId)
        {
            Guest? guest = await _guestRepository.GetByIdAsync(guestId);

            if (guest is null)
                return null;



            return guest.Bookings.Select(b => new BookingDto(
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
                )).ToList();
        }
    }
}
