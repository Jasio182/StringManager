using MediatR;
using StringManager.Services.API.Domain.Responses;
using System;

namespace StringManager.Services.API.Domain.Requests
{
    public class UpdateDatesForMyInstrumentRequest : IRequest<UpdateDatesForMyInstrumentResponse>
    {
        public int MyInstrumentId { get; set; }

        public DateTime Date { get; set; }
    }
}
