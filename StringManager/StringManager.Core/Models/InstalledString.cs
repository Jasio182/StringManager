using StringManager.Core.Enums;

namespace StringManager.Core.Models
{
    public class InstalledString
    {
        public int Id { get; set; }

        public int StringId { get; set; }

        public int ToneId { get; set; }

        public string Manufacturer { get; set; }

        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public int Position { get; set; }

        public string ToneName { get; set; }

        public int Frequency { get; set; }

        public int WaveLenght { get; set; }
    }
}
