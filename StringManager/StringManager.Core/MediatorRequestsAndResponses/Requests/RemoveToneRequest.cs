using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveToneRequest : RequestBase<Tone>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
