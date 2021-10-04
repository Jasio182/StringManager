namespace StringManager.Services.API.Domain.Requests
{
    public class AddTuningRequest : RequestBase
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
