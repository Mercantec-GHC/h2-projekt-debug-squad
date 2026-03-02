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
            return bookings.Select(b => new BookingSummaryDto
            {
                Id = b.Id,
                GuestId = b.Guest?.Id ?? 0,
                Room = b.Room != null ? new RoomDto(b.Room.Id, b.Room.Number) : null,
                GuestName = b.Guest?.FullName ?? "Unknown",
                RoomType = b.RoomType != null ? new RoomTypeDto(b.RoomType.Id, b.RoomType.Name, b.RoomType.Capacity, b.RoomType.PricePerNight) : null,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice
            }).ToList();
        }
    }
}
