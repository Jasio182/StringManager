using StringManager.Core.Enums;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringRequest : RequestBase<AddStringResponse>
    {
        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public int NumberOfDaysGood { get; set; }

        public int ManufacturerId { get; set; }
    }
}
