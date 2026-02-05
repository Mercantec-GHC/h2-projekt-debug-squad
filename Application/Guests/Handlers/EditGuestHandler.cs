using Application.Guests.Commands;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Guests.Handlers
{
    public class EditGuestHandler
    {
        private readonly IGuestRepository _repository;

        public EditGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EditGuestCommand command)
        {
            var guest = await _repository.GetByIdAsync(command.Id);
            if (guest == null)
                throw new Exception("Guest not found");

            guest.Change(command.FullName, command.PhoneNumber, command.Email);

            // Update guest details
            await _repository.EditAsync(guest, command.Id);
        }
    }
}
