using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentsRequest : RequestBase<GetMyInstrumentsResponse>
    {
        public int? RequestUserId { get; set; }
    }
}
