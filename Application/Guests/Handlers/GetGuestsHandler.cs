using Application.Guests.Queries;
using Application.Interfaces;
using Application.Guests.Queries;
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
            return guests.Select(guest => new GuestDto(guest.Id, guest.FullName, guest.PhoneNumber, guest.Email)).ToList();
        }
    }
}

