using StringManager.Core.Models;

namespace StringManager.Core.MediatorRequestsAndResponses.Requests
{
    public class AddManufacturerRequest : RequestBase<Manufacturer>
    {
        public string Name { get; set; }
    }
}
