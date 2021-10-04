namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyManufacturerRequest : RequestBase
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
