using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringTensionRequest : RequestBase<GetStringTensionResponse>
    {
        public int? StringId { get; set; }

        public int? ToneId { get; set; }

        public int? ScaleLenght { get; set; }
    }
}
