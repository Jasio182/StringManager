using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveMyInstrumentRequest : RequestBase<MyInstrument>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
