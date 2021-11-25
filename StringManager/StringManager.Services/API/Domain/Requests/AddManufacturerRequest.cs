using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddManufacturerRequest : RequestBase<Manufacturer>
    {
        public string Name { get; set; }
    }
}
