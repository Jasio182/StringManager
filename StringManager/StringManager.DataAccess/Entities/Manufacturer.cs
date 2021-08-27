using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Manufacturer : EntityBase
    {
        public string Name { get; set; }

        public IEnumerable<Instrument> Instruments { get; set; }

        public IEnumerable<String> Strings { get; set; }
    }
}
