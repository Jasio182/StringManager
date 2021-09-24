using MediatR;
using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddInstalledStringRequest : RequestBase<AddInstalledStringResponse>
    {
        public int Position { get; set; }

        public int MyInstrumentId { get; set; }

        public int StringId { get; set; }

        public int ToneId { get; set; }
    }
}
