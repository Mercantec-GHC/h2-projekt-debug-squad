using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DayMultipliers.Commands
{
    public class EditDayMultiplierCommand
    {
        public int Id { get; set; }
        public decimal Multiplier { get; set; }
    }
}
