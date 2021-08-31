using MediatR;
using StringManager.Core.Enums;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetStringsSetsRequest : IRequest<GetStringsSetsResponse>
    {
        public StringType StringType { get; set; }
    }
}
