using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class ModifyManufacturerRequest : RequestBase<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
