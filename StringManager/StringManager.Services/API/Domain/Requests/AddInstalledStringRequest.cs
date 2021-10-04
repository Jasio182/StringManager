namespace StringManager.Services.API.Domain.Requests
{
    public class AddInstalledStringRequest : RequestBase
    {
        public int Position { get; set; }

        public int MyInstrumentId { get; set; }

        public int StringId { get; set; }

        public int ToneId { get; set; }
    }
}
