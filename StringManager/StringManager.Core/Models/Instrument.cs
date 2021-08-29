
namespace StringManager.Core.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int NumberOfStrings { get; set; }

        public int ScaleLenghtBass { get; set; }

        public int ScaleLenghtTreble { get; set; }
    }
}
