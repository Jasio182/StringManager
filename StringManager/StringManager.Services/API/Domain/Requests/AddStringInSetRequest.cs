namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringInSetRequest : RequestBase
    {
        public int Position { get; set; }

        public int StringsSetId { get; set; }

        public int StringId { get; set; }
    }
}
