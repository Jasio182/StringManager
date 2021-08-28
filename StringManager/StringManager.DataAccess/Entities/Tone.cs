
using System.Collections.Generic;

namespace StringManager.DataAccess.Entities
{
    public class Tone : EntityBase
    {
        public string Name { get; set; }

        public int Frequency { get; set; }

        public int WaveLenght { get; set; }

        public IEnumerable<StringInSet> StringsInSets { get; set; }
    }
}
