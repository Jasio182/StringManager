using StringManager.Core.Enums;
using System;
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class MyInstrument : EntityBase
    {
        public string OwnName { get; set; }

        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }

        public IEnumerable<InstalledString> InstalledStrings { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int HoursPlayedWeekly { get; set; }

        public WhereGuitarKept GuitarPlace { get; set; }

        public bool NeededLuthierVisit { get; set; }

        public DateTime? LastDeepCleaning { get; set; }

        public DateTime? NextDeepCleaning { get; set; }

        public DateTime? LastStringChange { get; set; }

        public DateTime? NextStringChange { get; set; }
    }
}
