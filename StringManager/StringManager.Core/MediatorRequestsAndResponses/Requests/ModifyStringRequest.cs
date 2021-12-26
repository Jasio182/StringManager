using StringManager.Core.Enums;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyStringRequest : RequestBase<String>
    {
        public int Id { get; set; }

        public StringType? StringType { get; set; }

        public int? Size { get; set; }

        public double? SpecificWeight { get; set; }

        public int? NumberOfDaysGood { get; set; }

        public int? ManufacturerId { get; set; }
    }
}
