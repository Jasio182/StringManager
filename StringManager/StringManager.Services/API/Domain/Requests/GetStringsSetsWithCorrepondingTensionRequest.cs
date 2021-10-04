using StringManager.Core.Enums;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetsWithCorrepondingTensionRequest : RequestBase
    {
        public int? MyInstrumentId { get; set; }

        public StringType? StringType { get; set; }

        public int? ResultTuningId { get; set; }
    }
}
