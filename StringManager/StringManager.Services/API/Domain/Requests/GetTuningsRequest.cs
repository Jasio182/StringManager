using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetTuningsRequest : IRequest<GetTuningsResponse>
    {
        public int NumberOfStrings { get; set; }
    }
}
