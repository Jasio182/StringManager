namespace StringManager.Services.API.Domain.Requests
{
    public class GetMyInstrumentsRequest : RequestBase
    {
        public int? RequestUserId { get; set; }
    }
}
