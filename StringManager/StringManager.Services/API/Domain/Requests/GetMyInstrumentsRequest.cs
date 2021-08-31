using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentsRequest : IRequest<GetMyInstrumentsResponse>
    {
    }
}
