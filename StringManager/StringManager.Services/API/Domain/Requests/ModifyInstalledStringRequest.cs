namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyInstalledStringRequest : RequestBase
    {
        public int Id { get; set; }

        public int? StringId { get; set; }

        public int? ToneId { get; set; }
    }
}
