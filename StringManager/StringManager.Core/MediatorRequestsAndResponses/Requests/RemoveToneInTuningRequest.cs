using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveToneInTuningRequest : RequestBase<ToneInTuning>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
