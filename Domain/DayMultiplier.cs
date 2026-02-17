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
            Multiplier = multiplier;
        }
    }
}
