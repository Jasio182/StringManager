using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveInstrumentRequest : RequestBase<RemoveInstrumentResponse>
    {
        public int Id { get; set; }
    }
}
