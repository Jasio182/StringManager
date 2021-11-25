namespace StringManager.Services.API.Domain.Requests
{
    public class GetScaleLenghtsRequest : RequestBase<int[]>
    {
        public int? InstrumentId { get; set; }
    }
}
