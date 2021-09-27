using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class RemoveMyInstrumentRequest : RequestBase<RemoveMyInstrumentResponse>
    {
        public int Id { get; set; }
    }
}
