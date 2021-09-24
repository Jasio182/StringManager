using MediatR;
using StringManager.Core.Enums;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetsWithCorrepondingTensionRequest : RequestBase<GetStringsSetsWithCorrepondingTensionResponse>
    {
        public int? MyInstrumentId { get; set; }

        public StringType? StringType { get; set; }

        public int? ResultTuningId { get; set; }
    }
}
