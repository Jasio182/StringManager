using StringManager.Services.API.Domain.Responses;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyManufacturerRequest : RequestBase<ModifyManufacturerResponse>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
