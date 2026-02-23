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

        public async Task Handle(EditDayMultiplierCommand command)
        {
            DayMultiplier? dayMultiplier = await _repository.GetByIdAsync(command.Id);
            if (dayMultiplier == null) throw new ArgumentException("Day multiplier not found");

            dayMultiplier.ChangeMultiplier(command.Multiplier);
            await _repository.SaveChangesAsync();
        }
    }
}
