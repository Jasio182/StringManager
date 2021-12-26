using Microsoft.AspNetCore.Mvc;
using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class RemoveManufacturerRequest : RequestBase<Manufacturer>
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
