using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class GetTuningRequest : RequestBase<Tuning>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
