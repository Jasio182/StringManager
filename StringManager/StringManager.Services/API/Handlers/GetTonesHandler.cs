using MediatR;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StringManager.Services.API.Handlers
{
    class GetTonesHandler : IRequestHandler<GetTonesRequest, GetTonesResponse>
    {
        public GetTonesHandler()
        {

        }

        public Task<GetTonesResponse> Handle(GetTonesRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
