using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveTuningRequest : RequestBase<Tuning>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
