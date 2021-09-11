using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentRequest : IRequest<GetMyInstrumentResponse>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
