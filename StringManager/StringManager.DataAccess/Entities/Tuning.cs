using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Tuning : EntityBase
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }

        public IEnumerable<ToneInTuning> TonesInTuning { get; set; }
    }
}
