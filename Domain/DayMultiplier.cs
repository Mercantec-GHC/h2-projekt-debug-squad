using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DayMultiplier
    {
        public int Id { get; private set; }
        public DayOfWeek Day { get; private set; }
        public decimal Multiplier { get; private set; }

        private DayMultiplier() { }

        public void ChangeMultiplier(decimal multiplier)
        {
            if (multiplier <= 0) throw new ArgumentException("Multiplier must be greater than 0");
            Multiplier = multiplier;
        }
    }
}
