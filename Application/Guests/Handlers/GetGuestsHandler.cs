using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Guests.Queries;

namespace Application.Guests.Handlers
{
    public class GetGuestsHandler
    {
        private readonly IGuestRepository _repository;

        public GetGuestsHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Guest>> Handle(GetAllGuestsQuery query)
        {
            return await _repository.GetAllAsync();
        }
    }
}
