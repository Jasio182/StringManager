namespace StringManager.Services.API.Domain.Requests
{
    public class AddInstrumentRequest : RequestBase
    {
        public int ManufacturerId { get; set; }

        public string Model { get; set; }

        public int NumberOfStrings { get; set; }

        public int ScaleLenghtBass { get; set; }

        public int ScaleLenghtTreble { get; set; }
    }
}
