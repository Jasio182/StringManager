using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyManufacturerRequest : RequestBase<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
