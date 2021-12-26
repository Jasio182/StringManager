using StringManager.Core.Models;
using System.Collections.Generic;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetMyInstrumentsRequest : RequestBase<List<MyInstrumentList>>
    {
        public int? RequestUserId { get; set; }
    }
}
