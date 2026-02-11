using Application.Interfaces;
using Application.Rooms.Commands;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Bookings.Commands;

namespace Application.Bookings.Handlers
{
    public class CreateBookingHandler
    {
        //private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        //public CreateBookingHandler(IBookingRepository repository)
        //{
        //    _bookingRepository = repository;
        //}

        //public async Task Handle(CreateBookingCommand command)
        //{
        //    Booking booking = new Booking(command.Room, command.CheckInDate, command.CheckOutDate);

        //    await _bookingRepository.AddAsync(booking, command.GuestId);
        //}

        public CreateBookingHandler(IGuestRepository repository, IRoomRepository roomRepository)
        {
            _guestRepository = repository;
            _roomRepository = roomRepository;
        }

        public async Task? Handle(CreateBookingCommand command)
        {
            Room? room = await _roomRepository.GetByIdAsync(command.RoomId);
            //if (room == null) return null;
            Guest guest = await _guestRepository.GetByIdAsync(command.GuestId);

            Booking booking = new Booking(guest, room, command.CheckInDate, command.CheckOutDate);


            guest.AddBooking(booking);

            await _guestRepository.SaveChangesAsync();
        }
    }
}
