using Microsoft.AspNetCore.Mvc;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetScaleLenghtsRequest : RequestBase<int[]>
    {
        [FromRoute]
        public int InstrumentId { get; set; }
    }
}
