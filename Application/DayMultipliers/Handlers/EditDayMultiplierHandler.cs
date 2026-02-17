using Application.Interfaces;
using Domain;
using Application.DayMultipliers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DayMultipliers.Handlers
{
    public class EditDayMultiplierHandler
    {
        private readonly IDayMultiplierRepository _repository;

        public EditDayMultiplierHandler(IDayMultiplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(EditDayMultiplierCommand command)
        {
            DayMultiplier? dayMultiplier = await _repository.GetByIdAsync(command.Id);
            if (dayMultiplier == null) { return false; }

            dayMultiplier.ChangeMultiplier(command.Multiplier);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
