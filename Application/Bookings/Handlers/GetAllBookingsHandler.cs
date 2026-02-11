using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings.Handlers
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _repository;

        public GetAllBookingsHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookingDto>> Handle()
        {
            List<Booking> bookings = await _repository.GetAllAsync();

            return bookings.Select(b => new BookingDto(
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
