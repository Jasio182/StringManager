using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveInstrumentRequest : RequestBase<Instrument>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
