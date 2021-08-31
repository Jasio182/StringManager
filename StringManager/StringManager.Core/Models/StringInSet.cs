using StringManager.Core.Enums;

namespace StringManager.Core.Models
{
    public class StringInSet
    {
        public int Id { get; set; }

        public int StringId { get; set; }

        public string Manufacturer { get; set; }

        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public int Position { get; set; }
    }
}
