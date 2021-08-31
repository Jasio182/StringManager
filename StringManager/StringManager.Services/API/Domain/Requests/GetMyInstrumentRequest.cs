using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentRequest : IRequest<GetMyInstrumentResponse>
    {
        public int Id { get; set; }
    }
}
