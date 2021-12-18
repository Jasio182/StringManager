using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Instrument : EntityBase
    {
        public string Model { get; set; }

        public int NumberOfStrings { get; set; }

        public int ScaleLenghtBass { get; set; }

        public int ScaleLenghtTreble { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public IEnumerable<MyInstrument> MyInstruments { get; set; }
    }
}
