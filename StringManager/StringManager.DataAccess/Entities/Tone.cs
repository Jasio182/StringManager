
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Tone : EntityBase
    {
        public string Name { get; set; }

        public int Frequency { get; set; }

        public int WaveLenght { get; set; }

        public IEnumerable<InstalledString> InstalledStrings { get; set; }

        public IEnumerable<ToneInTuning> TonesInTuning { get; set; }
    }
}
