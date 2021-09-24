using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetScaleLenghtsRequest : RequestBase<GetScaleLenghtsResponse>
    {
        public int? InstrumentId { get; set; }
    }
}
