using Application.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bookings.Handlers
{
    public class GetBookingSummariesHandler
    {
        private readonly IBookingRepository _repository;

        public GetBookingSummariesHandler(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookingSummaryDto>> Handle()
        {
            var bookings = await _repository.GetAllAsync();
            return bookings.Select(b => new BookingSummaryDto(
                b.Id,
                b.Room != null ? new RoomDto(b.Room.Id, b.Room.Number) : null,
                b.Guest?.FullName ?? "Unknown",
                b.CheckInDate,
                b.CheckOutDate,
                b.TotalPrice
            )).ToList();
        }
    }
}
