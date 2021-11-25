using StringManager.Core.Enums;
using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringRequest : RequestBase<String>
    {
        public StringType StringType { get; set; }

        public int Size { get; set; }

        public double SpecificWeight { get; set; }

        public int NumberOfDaysGood { get; set; }

        public int ManufacturerId { get; set; }
    }
}
