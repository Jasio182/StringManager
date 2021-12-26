namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetScaleLenghtsRequest : RequestBase<int[]>
    {
        public int InstrumentId { get; set; }
    }
}
