using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyStringRequest : RequestBase
    {
        public int Id { get; set; }

        public StringType? StringType { get; set; }

        public int? Size { get; set; }

        public double? SpecificWeight { get; set; }

        public int? NumberOfDaysGood { get; set; }

        public int? ManufacturerId { get; set; }
    }
}
