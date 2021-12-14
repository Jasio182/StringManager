
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Tone : EntityBase
    {
        public string Name { get; set; }

        public double Frequency { get; set; }

        public double WaveLenght { get; set; }

        public IEnumerable<InstalledString> InstalledStrings { get; set; }

        public IEnumerable<ToneInTuning> TonesInTuning { get; set; }
    }
}
