using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringSizeWithCorrepondingTensionRequest : IRequest<GetStringSizeWithCorrepondingTensionResponse>
    {
        public int? ScaleLength { get; set; }

        public int? StringId { get; set; }

        public int? PrimaryToneId { get; set; }

        public int? ResultToneId { get; set; }
    }
}
