using StringManager.Core.Enums;

namespace StringManager.Core.Models
{
    public class String
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public int NumberOfDaysGood { get; set; }

        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }
    }
}
